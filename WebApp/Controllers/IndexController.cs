using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WepApp.Controllers
{
    public class IndexController : Controller
    {
        public IndexController()
        {
        }

        [Route("")]
        [HttpGet]
        public ContentResult Index()
        {
            try
            {
                var path = Path.Combine("./build", "index.html");
                var html = System.IO.File.ReadAllText(path, Encoding.UTF8);

                return new ContentResult
                {
                    Content = html,
                    ContentType = "text/html"
                };
            }
            catch
            {
                return new ContentResult
                {
                    Content = "Error",
                    ContentType = "text/html"
                };
            }
        }
    }
}