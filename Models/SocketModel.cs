using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class SocketModel
    {
        public string ServerIP { get; set; }
        public string LocalServerListeningIP { get; set; }
        public string LocalServerListeningPort { get; set; }
        public string ServerPort { get; set; }
        public string TextData { get; set; }
        public string TextDataServer { get; set; }
    }
}