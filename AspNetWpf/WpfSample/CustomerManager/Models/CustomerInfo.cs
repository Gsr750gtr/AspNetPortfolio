using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Models
{
    public record CustomerInfo(string Code, string Name, string NameKana, string Prefecture);
}
