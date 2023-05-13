namespace VotingApp.Infrastructure.InMemoryDatabase;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }
}