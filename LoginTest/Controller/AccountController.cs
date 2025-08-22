using LoginTest.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginTest.Controller
{
	public class AccountController
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;
		private RoleManager<IdentityRole> _roleManager;
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
		
		public async Task<IdentityResult> register(string username, string password)
		{
			var newUser = new AppUser
			{
				UserName = username
			};
			return await _userManager.CreateAsync(newUser,password);
		}

		public async Task<AppUser> getUser(string user)
		{
			return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == user.ToLower());
		}
		public async Task<List<string>> getRoles(string user)
		{
			AppUser appuser = await getUser(user);
			return (List<string>)await _userManager.GetRolesAsync(appuser);
		}
		public async Task addRole(string role)
		{
			var newRole = new IdentityRole(role);
			await _roleManager.CreateAsync(newRole);
		}
		public async Task<List<AppUser>> getUsers()
		{
			return await _userManager.Users.ToListAsync();
		}
		public async Task<List<IdentityRole>> getRoles()
		{
			return await _roleManager.Roles.ToListAsync();
		}
		public async Task deleteRole(string role)
		{
			IdentityRole newRole = await _roleManager.FindByNameAsync(role);
			await _roleManager.DeleteAsync(newRole);
		}
		public async Task assignRole(string user, string role)
		{
			AppUser appuser = await getUser(user);
			await _userManager.AddToRoleAsync(appuser, role);
		}
		
	}

}
