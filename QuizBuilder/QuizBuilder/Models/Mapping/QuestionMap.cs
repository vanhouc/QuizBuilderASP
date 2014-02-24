using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionID);

            // Properties
            this.Property(t => t.QuestionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.QuestionContent)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.AnswerContent)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Questions");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.ScenarioID).HasColumnName("ScenarioID");
            this.Property(t => t.QuestionTypeID).HasColumnName("QuestionTypeID");
            this.Property(t => t.QuestionContent).HasColumnName("QuestionContent");
            this.Property(t => t.AnswerContent).HasColumnName("AnswerContent");

            // Relationships
            this.HasRequired(t => t.Scenario)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.ScenarioID);

        }
    }
}
