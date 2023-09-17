using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Infrastructure.ApiIO
{
    public abstract class RequestBase<TRequest, TResponse> : IRequest<ApiResult<TResponse>>
    {
        public TRequest RequestData { get; set; }
        public string Language { get; set; }
    }
}
