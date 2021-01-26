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
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            Spiracle.NETCore.Models.FileModel fileModel = new Spiracle.NETCore.Models.FileModel();
            return View(fileModel);
        }

        public IActionResult ExecuteProcess(FileModel fileModel)
        {       
            var cmd = fileModel.Command;
            var cmdName = "";
            var cmdArgs = "";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                cmdName = cmd.Split(' ')[0];
                cmdArgs = cmd.Contains(' ') ? cmd.Remove(0, cmd.IndexOf(' ') + 1) : "";
            }
            else
            {
                var escapedArgs = cmd.Replace("\"", "\\\"");
                cmdName = "/bin/bash";
                cmdArgs = $"-c \"{escapedArgs}\"";
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = cmdName,
                    Arguments = cmdArgs,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            string result = "";

            try
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    result += (line + System.Environment.NewLine);
                }
            }

            catch (Exception e)
            {
                result += e;
            }

            fileModel.TextData = result;
            return View("Index", fileModel);
            
        }

        public IActionResult ReadWriteOrDeleteFile(FileModel fileModel)
        {
            try
            {
                if (fileModel.Operation.Contains("Read"))
                {
                    fileModel.TextData = System.IO.File.ReadAllText(@fileModel.PathInput, Encoding.UTF8);
                }
                else if (fileModel.Operation.Contains("Write"))
                {
                    using (StreamWriter sw = System.IO.File.AppendText(@fileModel.PathInput))
                    {
                        sw.WriteLine("Line appended from Spiracle.NETCore");
                    }
                    fileModel.TextData = "Wrote to file " + fileModel.PathInput;
                }
                else if (fileModel.Operation.Contains("Delete"))
                {
                    System.IO.File.Delete(@fileModel.PathInput);
                    fileModel.TextData = "Deleted file " + fileModel.PathInput;
                }
            }
            catch (Exception e)
            {
                fileModel.TextData = "" + e;
            }
            return View("Index", fileModel);
        }
    }
}