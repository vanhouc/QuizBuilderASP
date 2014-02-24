using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class QuizMap : EntityTypeConfiguration<Quiz>
    {
        public QuizMap()
        {
            // Primary Key
            this.HasKey(t => t.QuizID);

            // Properties
            this.Property(t => t.QuizID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.QuizAuthor)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.QuizName)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Quizzes");
            this.Property(t => t.QuizID).HasColumnName("QuizID");
            this.Property(t => t.QuizAuthor).HasColumnName("QuizAuthor");
            this.Property(t => t.QuizName).HasColumnName("QuizName");
            this.Property(t => t.QuestionPoolID).HasColumnName("QuestionPoolID");
        }
    }
}
