using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class FileModel
    {
        public string Command { get; set; }
        public string PathInput { get; set; }
        public string TextData { get; set; }
        public string Operation { get; set; }
        private List<string> operationList = new List<string>(new string[] {"Read", "Write", "Delete"});
        public List<string> OperationList
        {
            get => operationList;
        }
    }
}