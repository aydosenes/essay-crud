using System;
using System.Collections.Generic;
using System.Text;

namespace Essay.Core
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
