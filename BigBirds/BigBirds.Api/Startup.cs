using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using BigBirds.Api.DI;
using System.Data.Entity;
using BigBirds.Domain.Contexts;
using BigBirds.Domain.Entities.Seed;

[assembly: OwinStartup(typeof(BigBirds.Api.Startup))]

namespace BigBirds.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);

            // register DI
            Bootstrapper.Run();

            Database.SetInitializer<BigBirdsContext>(new EFBootstrap());
        }
    }
}
