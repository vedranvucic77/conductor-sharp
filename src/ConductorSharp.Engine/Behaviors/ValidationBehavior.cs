﻿using ConductorSharp.Engine.Util;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConductorSharp.Engine.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            ObjectValidator.Validate(request);

            var response = await next();

            ObjectValidator.Validate(response);

            return response;
        }
    }
}
