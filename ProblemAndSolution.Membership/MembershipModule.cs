using Autofac;
using DevSkill.Http.Utilities;
using DevSkill.Http.Emails.Services;
using ProblemAndSolution.Membership.Services;
using ProblemAndSolution.Membership.BusinessObj;

namespace ProblemAndSolution.Membership
{
    public class MembershipModule : Module
    {
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<UrlService>().As<IUrlService>()
				.InstancePerLifetimeScope();

			//builder.RegisterType<HtmlEmailService>().As<IQueuedEmailService>()
			//	.InstancePerLifetimeScope();

			builder.RegisterType<SignInManagerAdapter>().As<ISignInManagerAdapter<ApplicationUser>>()
				.InstancePerLifetimeScope();

			//builder.RegisterType<UserManagerAdapter>().As<IUserManagerAdapter<ApplicationUser>>()
			//	.InstancePerLifetimeScope();

			//builder.RegisterType<MembershipMailSenderService>().As<IMembershipMailSenderService>()
			//	.InstancePerLifetimeScope();
			base.Load(builder);
		}
	}
}
