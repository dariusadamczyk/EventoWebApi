using System;
using System.Threading;
using System.Threading.Tasks;
using Evento.Api.Features.Events;
using MediatR;

namespace Evento.Api.Features
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BrowseEventsQuery
    {
        
            public GenericPipelineBehavior()
            {
                
            }

            public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
            {
                Console.WriteLine("AAAA");
                var response = await next();
                Console.WriteLine("BBB");
                return response;
            }
        
    }
}
