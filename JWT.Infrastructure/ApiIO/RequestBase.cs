using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Infrastructure.ApiIO
{
    public abstract class RequestBase<T>
    {
        public T RequestData { get; set; }

    }
}
