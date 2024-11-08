using TajneedApi.Domain.Entities.CaseAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface ICaseRepository
{
    Task<Case> CreateCaseAsync(Case @case);
    
    
}
