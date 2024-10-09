using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TajneedApi.Application.Queries;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using Xunit;

public class GetMembersHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly GetMembers.Handler _handler;

    public GetMembersHandlerTests()
    {
        // Initialize the mock repository
        _mockMemberRepository = new Mock<IMemberRepository>();
        
        // Initialize the handler with the mock repository
        _handler = new GetMembers.Handler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMembers_WhenMembersExist()
    {
        // Arrange
        var query = new GetMembers.GetMembersQuery
        {
            JamaatId = "Jamaat123",
            CircuitId = "Circuit123",
            Page = 1,
            PageSize = 10
        };
        
        var memberList = new List<Member>
        {
            new Member("CN123", "MR123"),
            new Member("CN124", "MR124")
        };

        var paginatedMembers = new PaginatedList<Member>(){ Items = memberList, TotalItems = memberList.Count, Page = 1, PageSize = 10 };
        
        _mockMemberRepository
            .Setup(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId))
            .ReturnsAsync(paginatedMembers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        _mockMemberRepository.Verify(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoMembersExist()
    {
        // Arrange
        var query = new GetMembers.GetMembersQuery
        {
            JamaatId = "Jamaat123",
            CircuitId = "Circuit123",
            Page = 1,
            PageSize = 10
        };

        var emptyPaginatedMembers = new PaginatedList<Member>();

        _mockMemberRepository
            .Setup(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId))
            .ReturnsAsync(emptyPaginatedMembers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        _mockMemberRepository.Verify(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenValidRequest()
    {
        // Arrange
        var query = new GetMembers.GetMembersQuery
        {
            Page = 1,
            PageSize = 5,
            JamaatId = "JamaatId"
        };

        var members = new List<Member>
        {
            new Member("ChandaNo1", "MR123"),
            new Member("ChandaNo2", "MR124"),
        };
        
        var paginatedMembers = new PaginatedList<Member>(){ Items = members, TotalItems = members.Count, Page = 1, PageSize = 10 };

        _mockMemberRepository
            .Setup(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId))
            .ReturnsAsync(paginatedMembers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        _mockMemberRepository.Verify(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenInvalidQuery()
    {
        // Arrange
        var query = new GetMembers.GetMembersQuery
        {
            Page = -1,
            PageSize = 0
        };

        _mockMemberRepository
            .Setup(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId))
            .ReturnsAsync((PaginatedList<Member>?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _mockMemberRepository.Verify(repo => repo.GetMembersAsync(query, query.JamaatId, query.CircuitId), Times.Once);
    }
}
