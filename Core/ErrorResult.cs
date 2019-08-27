using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class ErrorResult
    {
        public string Title { get; }

        public ErrorResult(string title)
        {
            Title = title;
        }
    }
}
