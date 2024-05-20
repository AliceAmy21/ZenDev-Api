using Microsoft.EntityFrameworkCore;
using ZenDev.Migrations;
using ZenDev.Persistence;

Console.WriteLine("Running ZenDev migrations...");
Console.WriteLine("Do you want to seed data? y/n");
var choice = Console.ReadLine();

var connectionString = "Data Source=127.0.0.1\\ZenDev_DB,1433;database=ZenDev.Dev;User ID=SA;Password=ThreeAWithTAnd1J;TrustServerCertificate=true;";

using (var dbContext = new ZenDevDbContext(connectionString))
{
    dbContext.Database.Migrate();
    Console.WriteLine("Done with migrations......");

    if (choice == null || choice.ToLower() != "y") return;
    Console.WriteLine("Starting with seeding...");

    var dbSeeder = new DbSeeder(dbContext);
    dbSeeder.SeedData();

    Console.WriteLine("Done with seeding......");
}

Console.WriteLine("Done.");