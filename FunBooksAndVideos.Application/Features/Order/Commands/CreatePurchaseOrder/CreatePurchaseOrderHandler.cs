using System.Text.Json;
using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Domain.Enums;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, int>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly ICustomerAccountRepository _customerAccountRepository;
    private readonly IProductRepository _productRepository;

    public CreatePurchaseOrderHandler(IPurchaseOrderRepository purchaseOrderRepository, ICustomerAccountRepository customerAccountRepository, IProductRepository productRepository)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
        _customerAccountRepository = customerAccountRepository;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePurchaseOrderCommandValidator(_customerAccountRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Purchase Order", validationResult);
        }

        var purchaseOrder = new PurchaseOrder(request.CustomerId);

        var physicalItems = request.Items.Where(i => i.ItemLineType != ItemLineType.Membership).ToList();
        var membershipItems = request.Items.Where(i => i.ItemLineType == ItemLineType.Membership).ToList();

        var physicalProductItems = await GetPhysicalProductItems(physicalItems);
        if (physicalProductItems.Any())
        {
            purchaseOrder.AddItems(physicalProductItems);
            GenerateShippingSlip(purchaseOrder, physicalProductItems);
        }

        var membershipOrderItems = GetMembershipItems(membershipItems);
        if (membershipOrderItems.Any())
        {
            purchaseOrder.AddItems(membershipOrderItems);
            await ActivateMemberships(request.CustomerId, membershipOrderItems);
        }

        var created = await _purchaseOrderRepository.CreateAsync(purchaseOrder);
        return created.Id;
    }

    private async Task<ICollection<PurchaseOrderItem>> GetPhysicalProductItems(List<CreatePurchaseOrderItem> physicalItems)
    {
        var items = new List<PurchaseOrderItem>();
        
        foreach (var itemDto in physicalItems)
        {
            if (!itemDto.ProductId.HasValue)
            {
                throw new BadRequestException("Product ID must be specified for physical items");
            }

            var product = await GetPhysicalProduct(itemDto.ProductId.Value);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {itemDto.ProductId.Value} not found");
            }
            var item = new PurchaseOrderItem(itemDto.ItemLineType, product.Name, product.Price, product.Id);
            items.Add(item);
        }

        return items;
    }

    private void GenerateShippingSlip(PurchaseOrder purchaseOrder, ICollection<PurchaseOrderItem> physicalProductItems)
    {
        // BR2: Generate shipping slip for physical products
        if (!physicalProductItems.Any())
        {
            return; // No physical items to ship
        }

        var shippingSlip = new
        {
            GeneratedAt = DateTime.UtcNow,
            OrderId = purchaseOrder.Id,
            CustomerId = purchaseOrder.CustomerId,
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

        purchaseOrder.SetShippingSlipData(jsonData);
    }

    private ICollection<PurchaseOrderItem> GetMembershipItems(List<CreatePurchaseOrderItem> membershipItems)
    {
        var items = new List<PurchaseOrderItem>();
        foreach (var itemDto in membershipItems)
        {
            if (!itemDto.MembershipType.HasValue)
            {
                throw new BadRequestException("Membership type must be specified for membership items");
            }

            var membership = GetMembershipProduct(itemDto.MembershipType.Value);
            if(membership == null)
            {
                throw new NotFoundException($"Membership type {itemDto.MembershipType.Value} not found");
            }
            var item = new PurchaseOrderItem(ItemLineType.Membership, membership.Name, membership.Price, itemDto.MembershipType.Value);
            items.Add(item);
        }

        return items;
    }

    private async Task ActivateMemberships(int customerId, ICollection<PurchaseOrderItem> membershipItems)
    {
        if (membershipItems.Any())
        {
            // BR1: Activate memberships
            var customer = await _customerAccountRepository.GetByIdAsync(customerId);
            if(customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found");
            }

            foreach (var item in membershipItems)
            {
                if (!item.MembershipType.HasValue)
                {
                    throw new BadRequestException("Membership type must be specified for membership items");
                }

                customer.ActivateMembership(item.MembershipType.Value);
            }

            await _customerAccountRepository.UpdateAsync(customer);
        }
    }

    private async Task<Domain.Product?> GetPhysicalProduct(int productId) =>
        await _productRepository.GetByIdAsync(productId);

    private MembershipInfo? GetMembershipProduct(MembershipType membershipType) =>
        MembershipCatalog.All.FirstOrDefault(m => m.Type == membershipType);
}
