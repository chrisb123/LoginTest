using LoginTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using LoginTest.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LoginTest.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}
	}
}
