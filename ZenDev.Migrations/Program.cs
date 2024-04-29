using ZenDev.Persistence;

Console.WriteLine("Running ZenDev migrations...");
Console.WriteLine("This process currently COMPLETELY deletes and recreates the database. Are you sure you want to do this? y/n");
var choice = Console.ReadLine();
if (choice == null || choice.ToLower() != "y") return;

var connectionString = "Data Source=127.0.0.1\\ZenDev_DB,1433;database=ZenDev.Dev;User ID=SA;Password=ThreeAWithTAnd1J;TrustServerCertificate=true;";
using (var dbContext = new ZenDevDbContext(connectionString))
{
    Console.WriteLine("Deleting database...");
    dbContext.Database.EnsureDeleted();

    Console.WriteLine("Creating database...");
    dbContext.Database.EnsureCreated();
}

Console.WriteLine("Done.");