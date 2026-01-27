using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Models
{
    public class Result
    {
        public bool IsSuccess => Errors.Count == 0;
        public Dictionary<string, string[]> Errors { get; } = new();
    }

}
