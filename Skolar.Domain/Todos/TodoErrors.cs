using Skolar.Domain.Shared;

namespace Skolar.Domain.Todos;

public static class TodoErrors
{
    public static readonly Error TitleIsRequired =
        new("TODO_TITLE_REQUIRED", "Todo title is required.");

    public static readonly Error TitleTooShort =
        new("TODO_TITLE_TOO_SHORT", "Todo title must be at least 3 characters long.");

    public static readonly Error TitleTooLong =
        new("TODO_TITLE_TOO_LONG", "Todo title must not exceed 64 characters.");
}
