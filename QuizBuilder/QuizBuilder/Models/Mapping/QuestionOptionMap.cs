using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class QuestionOptionMap : EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionOptionID);

            // Properties
            this.Property(t => t.QuestionOptionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.OptionSequence)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.OptionText)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("QuestionOptions");
            this.Property(t => t.QuestionOptionID).HasColumnName("QuestionOptionID");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.OptionSequence).HasColumnName("OptionSequence");
            this.Property(t => t.OptionText).HasColumnName("OptionText");
            this.Property(t => t.IsRichText).HasColumnName("IsRichText");
            this.Property(t => t.IsCorrect).HasColumnName("IsCorrect");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.QuestionOptions)
                .HasForeignKey(d => d.QuestionID);

        }
    }
}
