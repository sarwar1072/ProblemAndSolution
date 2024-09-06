using Autofac;
using ProblemAndSolution.Web.Areas.ForPost.Models;
using ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder   ;
using ProblemAndSolution.Web.Models;

namespace ProblemAndSolution.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublicLayoutModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<RegistrationConfirmationModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<RegisterModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostLayoutModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionCreateModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionEditModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserProfileModel>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<AnswerCreateModel>()
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<BlogViewModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BlogModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateBlog>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FileHelper>().As<IFileHelper>().InstancePerLifetimeScope();
            builder.RegisterType<EditBlog>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType< UserProfileViewModel >().AsSelf().InstancePerLifetimeScope(); 

            base.Load(builder);
        }
    }
}
