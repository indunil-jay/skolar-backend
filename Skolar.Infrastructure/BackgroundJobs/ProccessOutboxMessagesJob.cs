using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;
using Skolar.Domain.Primitives;
using Skolar.Infrastructure.Outbox;

namespace Skolar.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProccessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProccessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
                               .Set<OutboxMessage>()
                               .Where(m => m.ProcessedOnUtc == null)
                               .Take(20)
                               .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            if (domainEvent is null)
            {
                //may be all logs
                continue;
            }


            AsyncPolicy policy = Policy.Handle<Exception>()
                                        .WaitAndRetryAsync(3, attempt=> TimeSpan.FromMicroseconds(50* attempt));

            PolicyResult policyResult = await policy.ExecuteAndCaptureAsync(() => _publisher.Publish(domainEvent, context.CancellationToken));

            message.Error = policyResult.FinalException?.ToString();
            message.ProcessedOnUtc = DateTime.UtcNow;

            //try
            //{
            //    await _publisher.Publish(domainEvent, context.CancellationToken);
            //    message.ProcessedOnUtc = DateTime.UtcNow;

            //}
            //catch (Exception e)
            //{
            //    message.Error = e.ToString();
            //    message.ProcessedOnUtc = DateTime.UtcNow;

            //}


        }

        await _dbContext.SaveChangesAsync();

    }
}
