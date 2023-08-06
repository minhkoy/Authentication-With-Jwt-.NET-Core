using MediatR;

namespace JWT.Infrastructure.ApiIO;

public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, ApiResult<TResponse>> 
    where TRequest : IRequest<ApiResult<TResponse>>
{
    public abstract Task<ApiResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}