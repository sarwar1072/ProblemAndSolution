using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Models
{
	public class RegistrationConfirmationModel
	{
		private ISignInManagerAdapter<ApplicationUser>? _signInManagerAdapter;
		private IUserManagerAdapter<ApplicationUser>? _userManagerAdapter;

		public string? Email { get; set; }

		public bool DisplayConfirmAccountLink { get; set; }

		public string? EmailConfirmationUrl { get; set; }

		public RegistrationConfirmationModel()
        {

        }

		public RegistrationConfirmationModel(ISignInManagerAdapter<ApplicationUser> signInManagerAdapter,
			IUserManagerAdapter<ApplicationUser> userManagerAdapter)
		{
			_signInManagerAdapter = signInManagerAdapter;
			_userManagerAdapter = userManagerAdapter;
		}
	}
}
