using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BigBirds.Api.Utils
{
    public static class ConfigConstants
    {
        public static readonly string WIT_ACCESS_TOKEN = WebConfigurationManager.AppSettings["bbhs:WitAIClientAccessToken"];
    }
}