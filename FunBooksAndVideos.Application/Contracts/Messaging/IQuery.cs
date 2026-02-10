using MediatR;

namespace FunBooksAndVideos.Application.Contracts.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
