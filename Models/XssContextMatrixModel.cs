using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class XssContextMatrixModel
    {
        public string Testcase;
        public string SinglePrint;
        public string Payload;
        public string TestcaseLabel;
        public string OutputText;

        public XssContextMatrixModel(string testcase, string singlePrint, string payload, string testcaseLabel, string outputText)
        {
            Testcase = testcase;
            SinglePrint = singlePrint;
            Payload = payload;
            TestcaseLabel = testcaseLabel;
            OutputText = outputText;
        }
    }
}