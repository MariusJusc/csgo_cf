using CoinFlip.Models;
using Data;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoinFlip.Main.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData Data;

        public BaseController(IUowData data)
        {
            this.Data = data;
        }


        public BaseController() :this(new UowData())
        {
        }

        /*public new HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }*/

/*
 *   <connectionStrings>
<add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-Libr.Main-20180515023039.mdf;Initial Catalog=aspnet-Libr.Main-20180515023039;Integrated Security=True"
providerName="System.Data.SqlClient" />
</connectionStrings>
*/
}
}
 