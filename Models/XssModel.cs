using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class XssModel
    {
        public List<string> RequestParamValues = new List<string>();

        public XssModel(List<string> requestParamValues)
        {
            RequestParamValues = requestParamValues;
        }
    }
}