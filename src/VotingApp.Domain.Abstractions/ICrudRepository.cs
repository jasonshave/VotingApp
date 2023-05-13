namespace VotingApp.Domain.Abstractions;

public interface ICrudRepository<TDomainModel>
    where TDomainModel : IAggregateRoot
{
    TDomainModel Save(TDomainModel model);

    TDomainModel Get(string id);
}
