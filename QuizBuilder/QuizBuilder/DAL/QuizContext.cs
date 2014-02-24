using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace QuizBuilder.Models
{
    public class QuizContext : DbContext
    {
        public QuizContext()
            : base("AzureConnection")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}