namespace FunBooksAndVideos.Application.Features.Purchase.Processing
{
    public interface IPurchaseOrderRule
    {
        bool IsApplicable(PurchaseOrderProcessingContext context);
        Task ApplyAsync(PurchaseOrderProcessingContext context, CancellationToken token);
    }
}