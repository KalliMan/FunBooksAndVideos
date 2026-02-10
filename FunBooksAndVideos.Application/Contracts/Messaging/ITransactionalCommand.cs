namespace FunBooksAndVideos.Application.Contracts.Messaging;
public interface ITransactionalCommand<out TResponse> : ICommand<TResponse> { }
