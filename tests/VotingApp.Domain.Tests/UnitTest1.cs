using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using VotingApp.Domain.Abstractions;

namespace VotingApp.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void VotingService_CreateWorkItem_Creates()
    {
        // arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        // act
        var workItem = subject.CreateWorkItem(host, "Test", true);

        // assert
        workItem.Should().NotBeNull();
        workItem.Id.Should().NotBeNull();
        workItem.Host.Should().BeSameAs(host);
    }

    [Fact]
    public void VotingService_EnableVoting_DoesNotThrow()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Returns(workItem);

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.EnableVoting(workItem.Id, host.Id)).Should().NotThrow();
    }

    [Fact(DisplayName ="WorkItem not found")]
    public void VotingService_EnableVoting_ThrowsNotFoundException()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Throws<NotFoundException>();

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.EnableVoting(workItem.Id, host.Id)).Should().Throw<NotFoundException>();
    }

    [Fact(DisplayName ="Participant is not host")]
    public void VotingService_EnableVoting_ThrowsApplicationException_ParticipantIsNotHost()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Returns(workItem);

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.EnableVoting(workItem.Id, "anotherId")).Should().Throw<ForbiddenException>();
    }

    [Fact]
    public void VotingService_DisableVoting_DoesNotThrow()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Returns(workItem);

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.DisableVoting(workItem.Id, host.Id)).Should().NotThrow();
    }

    [Fact(DisplayName = "WorkItem not found")]
    public void VotingService_DisableVoting_ThrowsNotFoundException()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Throws<NotFoundException>();

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.DisableVoting(workItem.Id, host.Id)).Should().Throw<NotFoundException>();
    }

    [Fact(DisplayName = "Participant is not host")]
    public void VotingService_DisableVoting_ThrowsApplicationException_ParticipantIsNotHost()
    {
        //arrange
        var host = new Participant("abc", "Frank");
        var mockWorkItemRepo = new Mock<IWorkItemRepository>();
        var mockLogger = new Mock<ILogger<VotingService>>();
        WorkItem workItem = new WorkItem()
        {
            Id = Guid.NewGuid().ToString(),
            Host = host,
            Name = "Test",
            VotingEnabled = false,
            IsAnonymous = true
        };
        mockWorkItemRepo.Setup(x => x.Save(It.IsAny<WorkItem>())).Returns((WorkItem y) => y);
        mockWorkItemRepo.Setup(x => x.Get(It.IsAny<string>())).Returns(workItem);

        var subject = new VotingService(mockWorkItemRepo.Object, mockLogger.Object);

        //act
        //assert
        subject.Invoking(x => x.DisableVoting(workItem.Id, "anotherId")).Should().Throw<ForbiddenException>();
    }
}
