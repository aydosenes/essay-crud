using System;
using System.Collections.Generic;
using System.Text;

namespace Essay.Core
{
    public class FailResult:GeneralResult
    {
        public FailResult() : base(Success:false)
        {
        }

        public FailResult(string message) : base(message, false)
        {
        }
       
    }
}
