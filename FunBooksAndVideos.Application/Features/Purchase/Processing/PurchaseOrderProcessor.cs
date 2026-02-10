namespace FunBooksAndVideos.Application.Features.Purchase.Processing;

public class PurchaseOrderProcessor
{
    private IEnumerable<IPurchaseOrderRule> Rules { get; }

    public PurchaseOrderProcessor(IEnumerable<IPurchaseOrderRule> rules)
    {
        Rules = rules;
    }

    public async Task ProcessAsync(PurchaseOrderProcessingContext context)
    {
        foreach (var rule in Rules)
        {
            if(rule.IsApplicable(context))
            {
                await rule.ApplyAsync(context);
            }
        }
    }
}
