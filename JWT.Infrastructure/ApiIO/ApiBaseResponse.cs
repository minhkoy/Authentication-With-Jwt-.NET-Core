using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Infrastructure.ApiIO
{
    public abstract class ApiBaseResponse
    {
        public ApiBaseResponse() { }
        public bool ApiSuccess { get; set; }
        public List<string> ApiMessage { get; set; } = new();
    }
}
