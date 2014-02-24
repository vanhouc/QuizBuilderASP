using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class QuizAttemptMap : EntityTypeConfiguration<QuizAttempt>
    {
        public QuizAttemptMap()
        {
            // Primary Key
            this.HasKey(t => t.QuizAttemptID);

            // Properties
            this.Property(t => t.QuizAttemptID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("QuizAttempts");
            this.Property(t => t.QuizAttemptID).HasColumnName("QuizAttemptID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.QuizID).HasColumnName("QuizID");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.QuizDate).HasColumnName("QuizDate");

            // Relationships
            this.HasRequired(t => t.Quizze)
                .WithMany(t => t.QuizAttempts)
                .HasForeignKey(d => d.QuizID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.QuizAttempts)
                .HasForeignKey(d => d.UserID);

        }
    }
}
