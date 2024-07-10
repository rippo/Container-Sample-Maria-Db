using FluentMigrator;

namespace Container.Infrastructure.Models;

[Migration(202407100002)]
public class SeedTables_202407100002 : Migration
{
    public override void Down()
    {
        Delete.FromTable("Employees");
        Delete.FromTable("Companies");
    }

    public override void Up()
    {
        Insert.IntoTable("Companies")
            .Row(new Company
            {
                Id = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                Address = "Test Address",
                Country = "USA",
                Name = "Ms. Jane"
            }).Row(new Company
            {
                Id = new Guid("67fbac34-1ee1-4697-b916-1748861dd276"),
                Address = "Test Address 2",
                Country = "UK",
                Name = "Mr Blobby"
            });

        Insert.IntoTable("Employees")
            .Row(new Employee
            {
                Id = new Guid("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b"),
                Age = 34,
                Name = "Test Employee",
                Position = "Test Position",
                CompanyId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275")
            });
    }
}