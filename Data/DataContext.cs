using Microsoft.EntityFrameworkCore;
using airtext_api.Models;


namespace airtext_api.Data;

public class DataContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connection = "Server=127.0.0.1;Database=airtext;User=root;Password=;";
		optionsBuilder.UseMySql(connection, new MySqlServerVersion(new Version(10, 6)));
		//optionsBuilder.UseInMemoryDatabase("Database");
	}


	public DbSet<User> Users { get; set; }
	public DbSet<Auth> Auth { get; set; }
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<UserAccount> UserAccounts { get; set; }

} 
