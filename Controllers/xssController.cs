using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Spiracle.NETCore.Models;
using System.Collections.Generic;
using System.Web;

namespace Spiracle.NETCore.Controllers
{
    public class xssController : Controller
    {
        public IActionResult Index()
        {
            List<string> requestParamValues = new List<string>();
            string requestParamKey = "";

            foreach (KeyValuePair<string, StringValues> paramKeyValues in Request.Query)
            {
                requestParamKey = paramKeyValues.Key;
                requestParamValues.Add(HttpContext.Request.Query[requestParamKey]);
            }

            XssModel xssModel = new XssModel(requestParamValues);  
            return View(xssModel);
        }
    }
}