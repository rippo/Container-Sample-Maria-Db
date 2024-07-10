using FluentMigrator;

namespace Container.Infrastructure.Models;

[Migration(202407100001)]
public class InitialTables_202407100001 : Migration
{
    public override void Down()
    {
        Delete.Table("employees");
        Delete.Table("companies");
    }

    public override void Up()
    {
        Create.Table("companies")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Address").AsString(60).NotNullable()
            .WithColumn("Country").AsString(50).NotNullable();
        Create.Table("employees")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Position").AsString(50).NotNullable()
            .WithColumn("CompanyId").AsGuid().NotNullable().ForeignKey("companies", "Id");
    }
}