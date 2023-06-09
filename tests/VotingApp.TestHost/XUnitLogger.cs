using Microsoft.Extensions.Logging;
using System.Text;
using Xunit.Abstractions;

namespace VotingApp.TestHost;

internal class XUnitLogger : ILogger
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly string? _categoryName;
    private readonly LoggerExternalScopeProvider _scopeProvider;

    public static ILogger CreateLogger(ITestOutputHelper testOutputHelper) => new XUnitLogger(testOutputHelper, new LoggerExternalScopeProvider(), "");
    public static ILogger<T> CreateLogger<T>(ITestOutputHelper testOutputHelper) => new XUnitLogger<T>(testOutputHelper, new LoggerExternalScopeProvider());

    public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider, string? categoryName)
    {
        _testOutputHelper = testOutputHelper;
        _scopeProvider = scopeProvider;
        _categoryName = categoryName;
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull => _scopeProvider.Push(state);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var sb = new StringBuilder();
        sb.Append(GetLogLevelString(logLevel))
          .Append(" [").Append(_categoryName).Append("] ")
          .Append(formatter(state, exception));

        if (exception is not null)
        {
            sb.Append('\n').Append(exception);
        }

        // Append scopes
        _scopeProvider.ForEachScope((scope, stringBuilder) =>
        {
            stringBuilder.Append("\n => ");
            stringBuilder.Append(scope);
        }, sb);

        _testOutputHelper.WriteLine(sb.ToString());
    }

    private static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "trace",
            LogLevel.Debug => "debug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "fail",
            LogLevel.Critical => "critical",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
        };
    }
}

internal sealed class XUnitLogger<T> : XUnitLogger, ILogger<T>
{
    public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider)
        : base(testOutputHelper, scopeProvider, typeof(T).FullName)
    {
    }
}