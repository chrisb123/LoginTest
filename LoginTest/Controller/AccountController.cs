using LoginTest.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginTest.Controller
{
	public class AccountController
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		
		public async Task Register(string username, string password)
		{
			var newUser = new AppUser
			{
				UserName = username
			};
			await _userManager.CreateAsync(newUser,password);
		}
	}
}
