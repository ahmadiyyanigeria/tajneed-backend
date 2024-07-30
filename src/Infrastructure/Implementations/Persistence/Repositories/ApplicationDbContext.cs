using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();
    public DbSet<AuxillaryBody> AuxillaryBodies => Set<AuxillaryBody>();
    public DbSet<BiodataUpdateCase> BiodataUpdateCases => Set<BiodataUpdateCase>();
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<CaseType> CaseTypes => Set<CaseType>();
    public DbSet<Circuit> Circuits => Set<Circuit>();
    public DbSet<Code> Forms => Set<Code>();
    public DbSet<CodeValue> Produces => Set<CodeValue>();
    public DbSet<DuplicateAccountCase> DuplicateAccountCases => Set<DuplicateAccountCase>();
    public DbSet<HouseHold> HouseHolds => Set<HouseHold>();
    public DbSet<HouseHoldMember> HouseHoldMembers => Set<HouseHoldMember>();
    public DbSet<Jamaat> Jamaats => Set<Jamaat>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<MemberMovement> MemberMovements => Set<MemberMovement>();
    public DbSet<MembershipStatus> MembershipStatus => Set<MembershipStatus>();    
    public DbSet<Nationality> Nationalities => Set<Nationality>();    
    public DbSet<Position> Positions => Set<Position>();    
    public DbSet<RelocationCase> RelocationCases => Set<RelocationCase>();    

}