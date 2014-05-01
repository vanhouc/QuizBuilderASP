using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using QuizBuilder.Models.Mapping;
using QuizBuilder.Models;

namespace QuizBuilder.DataContexts
{
    public partial class QuizBuilderDb : DbContext
    {
        static QuizBuilderDb()
        {
            Database.SetInitializer<QuizBuilderDb>(null);
        }

        public QuizBuilderDb()
            : base("Name=QuizBuilderDb")
        {
        }

        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new QuestionOptionMap());
            modelBuilder.Configurations.Add(new QuestionResponseMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new QuizAttemptMap());
            modelBuilder.Configurations.Add(new QuizMap());
            modelBuilder.Configurations.Add(new ScenarioMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
