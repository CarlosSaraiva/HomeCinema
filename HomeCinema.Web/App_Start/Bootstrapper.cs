using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeCinema.Web.App_Start
{
    internal class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure automapper
            //AutoMapperConfiguration.Configure();
        }
    }
}