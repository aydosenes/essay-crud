﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Essay.Core
{
    public class FailDataResult<T>:DataResult<T>
    {
        public FailDataResult(T data, string Message) : base(data, false, Message)
        {
        }

        public FailDataResult(T data) : base(data, false)
        {
        }

        public FailDataResult(string message) : base(default, false, message)
        {
        }
       
    }
}
