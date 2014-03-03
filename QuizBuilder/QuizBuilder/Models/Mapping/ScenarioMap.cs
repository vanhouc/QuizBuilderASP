using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QuizBuilder.Models.Mapping
{
    public class ScenarioMap : EntityTypeConfiguration<Scenario>
    {
        public ScenarioMap()
        {
            // Primary Key
            this.HasKey(t => t.ScenarioID);

            // Properties
            this.Property(t => t.ScenarioID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ScenarioName)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.ScenarioText)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Scenarios");
            this.Property(t => t.ScenarioID).HasColumnName("ScenarioID");
            this.Property(t => t.QuizID).HasColumnName("QuizID");
            this.Property(t => t.ScenarioSequence).HasColumnName("ScenarioSequence");
            this.Property(t => t.ScenarioName).HasColumnName("ScenarioName");
            this.Property(t => t.ScenarioText).HasColumnName("ScenarioText");
            this.Property(t => t.IsRichText).HasColumnName("IsRichText");

            // Relationships
            this.HasRequired(t => t.Quizze)
                .WithMany(t => t.Scenarios)
                .HasForeignKey(d => d.QuizID);

        }
    }
}
