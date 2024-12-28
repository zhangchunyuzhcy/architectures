using MediatR;
using Microsoft.AspNetCore.Http;
using MyProject.Domain.Common;
using MyProject.Infrastructure.Common.Persistence;

namespace MyProject.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, MyProjectDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();

        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                    value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // notify the client that even though they got a good response, the changes didn't take place
                // due to an unexpected error
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}