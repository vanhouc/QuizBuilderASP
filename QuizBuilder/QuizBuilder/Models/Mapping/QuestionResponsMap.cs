using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class QuestionResponsMap : EntityTypeConfiguration<QuestionRespons>
    {
        public QuestionResponsMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionResponseID);

            // Properties
            this.Property(t => t.QuestionResponseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("QuestionResponses");
            this.Property(t => t.QuestionResponseID).HasColumnName("QuestionResponseID");
            this.Property(t => t.QuestionOptionID).HasColumnName("QuestionOptionID");
            this.Property(t => t.QuizAttemptID).HasColumnName("QuizAttemptID");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.IsCorrect).HasColumnName("IsCorrect");
            this.Property(t => t.UserReviewFlag).HasColumnName("UserReviewFlag");

            // Relationships
            this.HasRequired(t => t.QuestionOption)
                .WithMany(t => t.QuestionResponses)
                .HasForeignKey(d => d.QuestionOptionID);
            this.HasRequired(t => t.Question)
                .WithMany(t => t.QuestionResponses)
                .HasForeignKey(d => d.QuestionID);
            this.HasRequired(t => t.QuizAttempt)
                .WithMany(t => t.QuestionResponses)
                .HasForeignKey(d => d.QuizAttemptID);

        }
    }
}
