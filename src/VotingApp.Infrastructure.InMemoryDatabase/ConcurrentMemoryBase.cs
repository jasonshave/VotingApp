using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using VotingApp.Domain.Abstractions;

namespace VotingApp.Infrastructure.InMemoryDatabase;

public abstract class ConcurrentMemoryBase<TData> : ICrudRepository<TData>
    where TData : IAggregateRoot
{
    private readonly ILogger _logger;
    private readonly ConcurrentDictionary<string, TData> _data = new();

    protected ConcurrentMemoryBase(ILogger logger)
    {
        _logger = logger;
    }

    public TData Save(TData data)
    {
        var success = _data.AddOrUpdate(data.Id, data, (_, _) => data);
        if (success is null)
        {
            _logger.LogSaveDataError(data.Id);
            throw new ApplicationException($"Unable to save data with ID {data.Id}");
        }

        _logger.LogDataSaved(data.Id);
        return data;
    }

    public TData Get(string id)
    {
        _data.TryGetValue(id, out var data);
        if (data is null)
        {
            _logger.LogGetDataError(id);
            throw new NotFoundException($"Unable to get data with ID {id}.");
        }

        return data;
    }
}