using Microsoft.Extensions.Logging;

namespace VotingApp.Infrastructure.InMemoryDatabase;

public static partial class LoggerExtensions
{
    [LoggerMessage(0, LogLevel.Error, "Unable to save data with ID: {id}.")]
    public static partial void LogSaveDataError(this ILogger logger, string id);

    [LoggerMessage(1, LogLevel.Error, "Unable to get data with ID: {id}.")]
    public static partial void LogGetDataError(this ILogger logger, string id);

    [LoggerMessage(2, LogLevel.Information, "Saved data with ID: {id}.")]
    public static partial void LogDataSaved(this ILogger logger, string id);
}