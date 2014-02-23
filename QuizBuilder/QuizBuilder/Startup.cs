using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizBuilder.Startup))]
namespace QuizBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
