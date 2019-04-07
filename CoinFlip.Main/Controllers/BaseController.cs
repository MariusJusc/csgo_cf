using Data;
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
    }
}
 