using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Spiracle.NETCore.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Spiracle.NETCore.Controllers
{
    public class path_traversalController : Controller
    {
        private IWebHostEnvironment _env;
        
        public IActionResult Index()
        {
            Spiracle.NETCore.Models.PathTraversalModel pathTraversalModel = new Spiracle.NETCore.Models.PathTraversalModel();
            return View(pathTraversalModel);
        }

        public path_traversalController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult File01(PathTraversalModel pathTraversalModel)
        {
            String outputTxt = "";
            var testFileName = pathTraversalModel.File01;
            var fileSeparator = Path.DirectorySeparatorChar;
            var relativePathToTestFilesParentDir = "pathTraversal" + fileSeparator + "testFilesParent";
            var relativePathToTestFilesChildDir = relativePathToTestFilesParentDir + fileSeparator + "testFilesChild" + fileSeparator;
            var webRoot = _env.WebRootPath;
            var absolutePathToTestFilesChildDir = System.IO.Path.Combine(Directory.GetParent(webRoot).ToString(), relativePathToTestFilesChildDir);   
            var absolutePathToTestFile = absolutePathToTestFilesChildDir + testFileName;

            outputTxt += "Access to the following file created: " + absolutePathToTestFile + Environment.NewLine;

            var fileExistsMessage = Environment.NewLine + "And file already exists.";
            var fileDoesNotExistMessage = Environment.NewLine + "But file does not exist.";

            if (System.IO.File.Exists(absolutePathToTestFile))
            {
                outputTxt += fileExistsMessage;
            }
            else
            {
                outputTxt += fileDoesNotExistMessage;
            }

            pathTraversalModel.OutputText = outputTxt;
            return View("Index", pathTraversalModel);  
        }

        public IActionResult File02(PathTraversalModel pathTraversalModel)
        {
            String outputTxt = "";
            var testFileName = pathTraversalModel.File02;
            var fileSeparator = Path.DirectorySeparatorChar;
            var relativePathToTestFilesParentDir = "pathTraversal" + fileSeparator + "testFilesParent";
            var relativePathToTestFilesChildDir = relativePathToTestFilesParentDir + fileSeparator + "testFilesChild" + fileSeparator;
            var webRoot = _env.WebRootPath;
            var absolutePathToTestFilesChildDir = System.IO.Path.Combine(Directory.GetParent(webRoot).ToString(), relativePathToTestFilesChildDir);   
            var absolutePathToTestFile = absolutePathToTestFilesChildDir + "." + testFileName;

            outputTxt += "Access to the following file created: " + absolutePathToTestFile + Environment.NewLine;

            var fileExistsMessage = Environment.NewLine + "And file already exists.";
            var fileDoesNotExistMessage = Environment.NewLine + "But file does not exist.";

            if (System.IO.File.Exists(absolutePathToTestFile))
            {
                outputTxt += fileExistsMessage;
            }
            else
            {
                outputTxt += fileDoesNotExistMessage;
            }

            pathTraversalModel.OutputText = outputTxt;
            return View("Index", pathTraversalModel); 
        }

        public IActionResult File03(PathTraversalModel pathTraversalModel)
        {
            String outputTxt = "";
            var testFileName = pathTraversalModel.File03;
            var absolutePathToTestFile = testFileName;

            var fileExistsMessage = Environment.NewLine + "And file already exists.";
            var fileDoesNotExistMessage = Environment.NewLine + "But file does not exist.";

            if (System.IO.File.Exists(absolutePathToTestFile))
            {
                outputTxt += fileExistsMessage;
            }
            else
            {
                outputTxt += fileDoesNotExistMessage;
            }

            pathTraversalModel.OutputText = outputTxt;;
            return View("Index", pathTraversalModel);  
        }
    }
}