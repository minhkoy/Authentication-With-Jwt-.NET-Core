using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Infrastructure.ApiIO
{
    public class ApiResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public object ErrorList { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        //public E ErrorValidations { get; set; }
    }
}
