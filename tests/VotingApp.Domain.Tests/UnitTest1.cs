namespace VotingApp.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void VotingService_CreateWorkItem_Creates()
    {
        // arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        var subject = new VotingService(mockWorkItemRepo.Object);

        // act
        var workItem = subject.CreateWorkItem(host, "Test", true);

        // assert
        workItem.Should().NotBeNull();
        workItem.Id.Should().NotBeNull();
        workItem.Host.Should().BeSameAs(host);
    }
}
