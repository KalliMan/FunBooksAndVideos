using System.Text.Json;

namespace FunBooksAndVideos.Application.Features.Purchase.Processing;

/// <summary>
/// Rule to generate a shipping slip for physical products in the purchase order.
/// It creates a JSON representation of the shipping slip and attaches it to the purchase order context.
/// </summary>
public class GenerateShippingSlipRule : IPurchaseOrderRule
{
    public async Task ApplyAsync(PurchaseOrderProcessingContext context)
    {
        var physicalProductItems = context.PhysicalItems;        
        if (!physicalProductItems.Any())
        {
            return;
        }

        var shippingSlip = new
        {
            GeneratedAt = DateTime.UtcNow,
            context.CustomerId,
            Items = physicalProductItems.Select(item => new
            {
                item.Name,
                item.Price,
                ItemType = item.ItemLineType.ToString()
            }).ToList()
        };

        var jsonData = JsonSerializer.Serialize(shippingSlip, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        context.PurchaseOrder.SetShippingSlipData(jsonData);

        await Task.CompletedTask;
    }

    public bool IsApplicable(PurchaseOrderProcessingContext context) =>
        context.PhysicalItems != null && context.PhysicalItems.Any();
}
