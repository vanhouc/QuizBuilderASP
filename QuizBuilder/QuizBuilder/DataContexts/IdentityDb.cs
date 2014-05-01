using Microsoft.AspNet.Identity.EntityFramework;
using QuizBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBuilder.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("IdentityDb")
        {
        }
    }
}