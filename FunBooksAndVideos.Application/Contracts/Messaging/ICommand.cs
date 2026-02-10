using MediatR;

namespace FunBooksAndVideos.Application.Contracts.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse> { }