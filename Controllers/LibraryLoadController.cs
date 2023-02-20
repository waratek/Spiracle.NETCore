using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spiracle.NETCore.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Spiracle.NETCore.Controllers
{
    public class LibraryLoadController : Controller
    {
        public IActionResult Index()
        {
            return View(new LibraryLoadModel());
        }

        [Route("/libraryLoad/load")]
        [HttpPost]
        public IActionResult NativeLibraryLoad(LibraryLoadModel model)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , model.LibraryName);
                if(!System.IO.File.Exists(path))
                {
                    throw new FileNotFoundException($"File not found for path: '{path}'");
                }

                model.OperationResult = PerformLoad(path, model.SelectedLibraryLoadMethod);

            }
            catch( Exception ex)
            {
                model.OperationResult = "EXCEPTION: " + ex.Message;
            }

            return View("Index", model);
        }


        private string PerformLoad(string filePath, string loadType)
        {
            switch (loadType)
            {
                case "NativeLibrary::Load":
                    {
                        return NativeLibraryLoad(filePath);
                    }

                case "NativeLibrary::TryLoad":
                    {
                        return NativeLibraryTryLoad(filePath);
                    }

                case "System.Reflection.Assembly::LoadFile":
                    {
                        return SystemReflectionAssemblyLoadFile(filePath);
                    }

                case "System.Reflection.Assembly::LoadFrom":
                    {

                        return SystemReflectionAssemblyLoadFrom(filePath);
                    }

                default:
                    return "Error! Unknown Library Load Method";
            }
        }
        private string NativeLibraryLoad(string filePath)
        {
             return $"Library loaded. IntPtr = '{NativeLibrary.Load(filePath)}'";
        }

        private string NativeLibraryTryLoad(string filePath)
        {
            NativeLibrary.TryLoad(filePath, out var handle);
            return $"Library loaded. IntPtr = '{handle}'";
        }

        private string SystemReflectionAssemblyLoadFile(string filePath)
        {
            var assembly = System.Reflection.Assembly.LoadFile(filePath);
            return $"Library loaded. AssemblyName = '{assembly.FullName}'";
        }

        private string SystemReflectionAssemblyLoadFrom(string filePath)
        {
            var assembly = System.Reflection.Assembly.LoadFrom(filePath);
            return $"Library loaded. AssemblyName = '{assembly.FullName}'";
        }

    }
}
