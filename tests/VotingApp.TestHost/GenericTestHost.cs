using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace VotingApp.TestHost;

public abstract class GenericTestHost
{
    protected readonly IHost _host;

    protected GenericTestHost(ITestOutputHelper testOutputHelper, Action<IServiceCollection>? servicesDelegate = null)
    {
        _host = Host
            .CreateDefaultBuilder()
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(testOutputHelper));
            })
            .ConfigureServices(services =>
            {
                servicesDelegate?.Invoke(services);
            })
            .Build();
    }
}