using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Infrastructure.ApiIO
{
    public class RequestNoInput<TResponse> : IRequest<ApiResult<TResponse>>
    {
    }
}
