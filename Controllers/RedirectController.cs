using Microsoft.AspNetCore.Mvc;

namespace Spiracle.NETCore.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            string redirectType = HttpContext.Request.Query["redirectType"];
            string value = HttpContext.Request.Query["value"];

            switch (redirectType)
            {
                case "Redirect":
                case "ResponseRedirect":               
                    return Redirect("" + value);

                case "RedirectPermanent":
                case "ResponseRedirectLocation":  
                    return RedirectPermanent("" + value);

                case "RedirectPreserveMethod":   
                    return RedirectPreserveMethod("" + value);

                case "RedirectPermanentPreserveMethod": 
                    return RedirectPermanentPreserveMethod("" + value);

                case "PartiallyHardcodedResponseRedirectToGoo":
                {
                    return Redirect("http://www.goo" + value);
                }

                case "PartiallyHardcodedResponseRedirectTo":
                {
                    return Redirect("http://" + value);
                }


                case "HardcodedResponseRedirectToGoogle":
                    return Redirect("http://www.google.com");

                case "RedirectToAction":
                    return RedirectToAction("" + value);

                case "RedirectToPage": 
                    return RedirectToPage("" + value);

                case "RedirectToRoute": 
                    return RedirectToRoute(new { controller = "Redirect", action = "" + value });

                case "LocalRedirect":
                    return LocalRedirect("" + value);

                default:
                    return View();
            }
        }
        public string TestAction()
        {
            return "TestAction invoked";
        }
    }
}