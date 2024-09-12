namespace TajneedApi.Application.Repositories;

public interface IDatabaseInitializer
{
    Task SeedDatas();
}