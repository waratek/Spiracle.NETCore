using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Spiracle.NETCore.Models;
using System.Collections.Generic;
using System.Web;

namespace Spiracle.NETCore.Controllers
{
    public class xssContextMatrixController : Controller
    {
        public IActionResult Index()
        {
            List<string> requestParamValues = new List<string>();
            string testcase;
            string singlePrint;
            bool singlePrintBoolean;
            string payload;
            string testcaseLabel;
            string outputText = "";

            string requestParamKey = "";

            foreach (KeyValuePair<string, StringValues> paramKeyValues in Request.Query)
            {
                requestParamKey = paramKeyValues.Key;
                requestParamValues.Add(HttpContext.Request.Query[requestParamKey]);
            }
            testcase = requestParamValues[0];
            singlePrint = requestParamValues[1];
            payload = requestParamValues[2];

            if (singlePrint.Equals("off"))
            {
                singlePrintBoolean = false;
            }
            else
            {
                singlePrintBoolean = true;
            }

            switch(testcase)
            {
                case "1":
                    testcaseLabel = "Test Case #1: Content (No concatenation)";
                    outputText = payload;
                break;
                
                case "2":
                    testcaseLabel = "Test Case #2: Double-quoted Attr (No concatenation)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\"" + payload + "\" />";
                    }
                    else
                    {
                        outputText += "<img src=\"";
                        outputText += payload;
                        outputText += "\" />";
                    }
                break;

                case "3":
                    testcaseLabel = "Test Case #3: Single-quoted Attr (No concatenation)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\'" + payload + "\' />";
                    }
                    else
                    {
                        outputText += "<img src=\'";
                        outputText += payload;
                        outputText += "\' />";
                    }
                break;

                case "4":
                    testcaseLabel = "Test Case #4: Unquoted Attr (No concatenation)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=" + payload + " />";
                    }
                    else
                    {
                        outputText += "<img src=";
                        outputText += payload;
                        outputText += " />";
                    }
                break;

                case "5":
                    testcaseLabel = "Test Case #5: Attribute Name (No concatenation)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img " + payload + "='100' />";
                    }
                    else
                    {
                        outputText += "<img ";
                        outputText += payload;
                        outputText += "='100' />";
                    }
                break;

                case "6":
                    testcaseLabel = "Test Case #6: Content (Payload as Prefix)";
                    if (singlePrintBoolean)
                    {
                        outputText = payload + "foo";
                    }
                    else
                    {
                        outputText += payload;
                        outputText += "foo";
                    }
                break;

                case "7":
                    testcaseLabel = "Test Case #7: Double-quoted Attr (Payload as Prefix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\"" + payload + "foo\" />";
                    }
                    else
                    {
                        outputText += "<img src=\"";
                        outputText += payload;
                        outputText += "foo\" />";
                    }
                break;

                case "8":
                    testcaseLabel = "Test Case #8: Single-quoted Attr (Payload as Prefix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\'" + payload + "foo\' />";
                    }
                    else
                    {
                        outputText += "<img src=\'";
                        outputText += payload;
                        outputText += "foo\' />";
                    }
                break;

                case "9":
                    testcaseLabel = "Test Case #9: Unquoted Attr (Payload as Prefix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=" + payload + "foo />";
                    }
                    else
                    {
                        outputText += "<img src=";
                        outputText += payload;
                        outputText += "foo />";
                    }
                break;

                case "10":
                    testcaseLabel = "Test Case #10: Attribute Name (Payload as Prefix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img " + payload + "foo='100' />";
                    }
                    else
                    {
                        outputText += "<img ";
                        outputText += payload;
                        outputText += "foo='100' />";
                    }
                break;

                case "11":
                    testcaseLabel = "Test Case #11: Content (Payload as Suffix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "foo" + payload;
                    }
                    else
                    {
                        outputText += "foo";
                        outputText += payload;
                    }
                break;

                case "12":
                    testcaseLabel = "Test Case #12: Double-quoted Attr (Payload as Suffix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\"foo" + payload + "\" />";
                    }
                    else
                    {
                        outputText += "<img src=\"foo";
                        outputText += payload;
                        outputText += "\" />";
                    }
                break;

                case "13":
                    testcaseLabel = "Test Case #13: Single-quoted Attr (Payload as Suffix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=\'foo" + payload + "\' />";
                    }
                    else
                    {
                        outputText += "<img src=\'foo";
                        outputText += payload;
                        outputText += "\' />";
                    }
                break;

                case "14":
                    testcaseLabel = "Test Case #14: Unquoted Attr (Payload as Suffix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img src=foo" + payload + " />";
                    }
                    else
                    {
                        outputText += "<img src=foo";
                        outputText += payload;
                        outputText += " />";
                    }
                break;

                case "15":
                    testcaseLabel = "Test Case #15: Unquoted Attr (Payload as Suffix)";
                    if (singlePrintBoolean)
                    {
                        outputText = "<img foo" + payload + "='100' />";
                    }
                    else
                    {
                        outputText += "<img foo";
                        outputText += payload;
                        outputText += "='100' />";
                    }
                break;

                default:
                    testcaseLabel = "";
                    outputText = "Invalid value selected for testcase param. Value should be between 1 and 15, inclusive";
                    break;
            }

            XssContextMatrixModel xssContextMatrixModel = new XssContextMatrixModel(testcase, singlePrint, payload, testcaseLabel, outputText);  
            return View(xssContextMatrixModel);
        }
    }
}