using System.Collections.Concurrent;
using VotingApp.Domain.Abstractions;

namespace VotingApp.Infrastructure.InMemoryDatabase
{
    public abstract class InMemoryCrudRepository<TDomainModel, TPersistedModel> : ICrudRepository<TDomainModel>
        where TDomainModel : IAggregateRoot
    {
        private readonly ConcurrentDictionary<string, TPersistedModel> _storage = new();
        public TDomainModel? Get(string id)
        {
            throw new NotImplementedException();
        }

        public TDomainModel Save(TDomainModel model)
        {
            throw new NotImplementedException();
        }
    }
}
