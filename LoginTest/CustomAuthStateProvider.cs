using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace LoginTest
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		private AuthenticationState authenticationState;

		public CustomAuthStateProvider(CustomAuthenticationService service)
		{
			authenticationState = new AuthenticationState(service.CurrentUser);

			service.UserChanged += (newUser) =>
			{
				authenticationState = new AuthenticationState(newUser);
				NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
			};
		}

		public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
			Task.FromResult(authenticationState);
	}
	
	public class CustomAuthenticationService
	{
		public event Action<ClaimsPrincipal>? UserChanged;
		private ClaimsPrincipal? currentUser;

		public ClaimsPrincipal CurrentUser
		{
			get { return currentUser ?? new(); }
			set
			{
				currentUser = value;

				if (UserChanged is not null)
				{
					UserChanged(currentUser);
				}
			}
		}
	}
	public class ExampleService
	{
		public async Task<string> ExampleMethod(AuthenticationStateProvider authStateProvider)
		{
			var authState = await authStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;
			Console.WriteLine("---------- auth state user ---------------");
			Console.WriteLine(user);
			if (user.Identity is not null && user.Identity.IsAuthenticated)
			{
				return $"{user.Identity.Name} is authenticated.";
			}
			else
			{
				return "The user is NOT authenticated.";
			}
		}
	}
}