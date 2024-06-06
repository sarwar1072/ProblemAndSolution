using Autofac;
using ProblemAndSolution.Infrastructure.DbContexts;
using ProblemAndSolution.Infrastructure.Repositories;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Infrastructure.UnitOfWorks;

namespace ProblemAndSolution.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly string _webHostEnvironment;

        public InfrastructureModule(string connectionString, string migrationAssemblyName,
            string webHostEnvironment)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
            _webHostEnvironment = webHostEnvironment;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<PAndSUnitOfWork>().As<IPAndSUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<QuestionServices>().As<IQuestionServices>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentRepository>().As<ICommentRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AnswerServices>().As<IAnswerServices>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VoteRepository>().As<IVoteRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<BlogRepository>().As<IBlogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BlogServices>().As<IBlogServices>().InstancePerLifetimeScope();    

            base.Load(builder);
        }
    }
}
