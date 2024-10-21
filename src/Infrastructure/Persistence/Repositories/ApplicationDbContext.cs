using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.CaseAggregateRoot;
using TajneedApi.Domain.Entities.CodeAggregateRoot;
using TajneedApi.Domain.Entities.HouseHoldAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Infrastructure.Persistence.Repositories;
public class ApplicationDbContext(
    ICurrentUser currentUser,
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected readonly ICurrentUser _currentUser = currentUser;

    public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();
    public DbSet<AuxiliaryBody> AuxiliaryBodies => Set<AuxiliaryBody>();
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<Circuit> Circuits => Set<Circuit>();
    public DbSet<Code> Forms => Set<Code>();
    public DbSet<CodeValue> Produces => Set<CodeValue>();
    public DbSet<HouseHold> HouseHolds => Set<HouseHold>();
    public DbSet<HouseHoldMember> HouseHoldMembers => Set<HouseHoldMember>();
    public DbSet<Jamaat> Jamaats => Set<Jamaat>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<MemberMovement> MemberMovements => Set<MemberMovement>();
    public DbSet<MembershipRequest> MembershipRequests => Set<MembershipRequest>();
    public DbSet<Nationality> Nationalities => Set<Nationality>();
    public DbSet<Position> Positions => Set<Position>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(s => !s.IsDeleted);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<BiodataUpdateCase>();
        modelBuilder.Ignore<DuplicateAccountCase>();
        modelBuilder.Ignore<RelocationCase>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        HandleAuditingBeforeSaveChanges(_currentUser.GetUserDetails().UserId);
        int result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    private void HandleAuditingBeforeSaveChanges(string userId)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
        ChangeTracker.DetectChanges();
    }
}


internal static class ModelBuilderExtensions
{
    public static ModelBuilder AppendGlobalQueryFilter<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> filter)
    {
        // get a list of entities without a baseType that implement the interface TInterface
        var entities = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.BaseType is null && e.ClrType.GetInterface(typeof(TInterface).Name) is not null)
            .Select(e => e.ClrType);

        foreach (var entity in entities)
        {
            var parameterType = Expression.Parameter(modelBuilder.Entity(entity).Metadata.ClrType);
            var filterBody = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), parameterType, filter.Body);

            // get the existing query filter
            if (modelBuilder.Entity(entity).Metadata.GetQueryFilter() is { } existingFilter)
            {
                var existingFilterBody = ReplacingExpressionVisitor.Replace(existingFilter.Parameters.Single(), parameterType, existingFilter.Body);

                // combine the existing query filter with the new query filter
                filterBody = Expression.AndAlso(existingFilterBody, filterBody);
            }

            // apply the new query filter
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(filterBody, parameterType));
        }

        return modelBuilder;
    }
}