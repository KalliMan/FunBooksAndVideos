using FunBooksAndVideos.Application.Contracts.Messaging;
using FunBooksAndVideos.Application.Contracts.Persistence;
using MediatR;

namespace FunBooksAndVideos.Application.Behaviors;

public class SaveChangesBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveChangesBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICommand<TResponse>)
        {
            return await next();
        }

        if (request is ITransactionalCommand<TResponse>)
        {
            return await next();
        }

        var response = await next();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return response;
    }
}
