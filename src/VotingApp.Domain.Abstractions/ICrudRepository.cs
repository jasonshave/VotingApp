namespace VotingApp.Domain.Abstractions;

public interface ICrudRepository<TDomainModel>
    where TDomainModel : IAggregateRoot
{
    /// <summary>
    /// Saves domain model or throws.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    TDomainModel Save(TDomainModel model);

    /// <summary>
    /// Returns domain model or an exception.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    TDomainModel Get(string id);
}
