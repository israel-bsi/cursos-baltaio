using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Request.Order;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class OrderHandler(AppDbContext context) : IOrderHandler
{
    public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

            if (order is null)
                return new Response<Order?>(null, 404, "Pedido não encontrado");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Canceled:
                return new Response<Order?>(null, 400, "Pedido já cancelado");
            case EOrderStatus.WaitingPayment:
                break;
            case EOrderStatus.Paid:
                return new Response<Order?>(null, 400, "Pedido já pago e não pode ser cancelado");
            case EOrderStatus.Refunded:
                return new Response<Order?>(null, 400, "Pedido já foi reembolsado e não pode ser cancelado");
            default:
                return new Response<Order?>(null, 400, "Pedido não pode ser cancelado");
        }

        order.Status = EOrderStatus.Canceled;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível cancelar seu pedido");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} cancelado com sucesso");
    }

    public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
    {
        Product? product;
        try
        {
            product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ProductId && x.IsActive == true);

            if (product is null)
                return new Response<Order?>(null, 404, "Produto não encontrado");

            context.Attach(product);
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter produto");
        }

        Voucher? voucher = null;

        try
        {
            if (request.VoucherId is not null)
            {
                voucher = await context.Vouchers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsActive == true);

                if (voucher is null)
                    return new Response<Order?>(null, 400, "Voucher inválido ou não encontrado");

                if (voucher.IsActive == false)
                    return new Response<Order?>(null, 400, "Esse voucher já foi utilizado");

                voucher.IsActive = false;
                context.Vouchers.Update(voucher);

            }   
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter o voucher informado");
        }

        var order = new Order
        {
            ProductId = product.Id,
            Product = product,
            VoucherId = voucher?.Id,
            Voucher = voucher,
            UserId = request.UserId
        };

        try
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível realizar seu pedido");
        }

        return new Response<Order?>(order, 201, $"Pedido {order.Number} realizado com sucesso");
    }

    public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
    {
        Order? order;

        try
        {
            order = await context.Orders
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

            if (order is null)
                return new Response<Order?>(null, 404, "Pedido não encontrado");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Canceled:
                return new Response<Order?>(null, 400, "Pedido cancelado e não pode ser pago");
            case EOrderStatus.Paid:
                return new Response<Order?>(null, 400, "Este pedido já está pago");
            case EOrderStatus.Refunded:
                return new Response<Order?>(null, 400, "Pedido reembolsado e não pode ser pago");
            case EOrderStatus.WaitingPayment:
                break;
            default:
                return new Response<Order?>(null, 400, "Pedido não pode ser pago");
        }

        order.Status = EOrderStatus.Paid;
        order.ExternalReference = request.ExternalReference;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível pagar seu pedido");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} pago com sucesso");
    }

    public async Task<Response<Order?>> RefundAsync(RefundOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

            if (order is null)
                return new Response<Order?>(null, 404, "Pedido não encontrado");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Falha ao obter pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Canceled:
                return new Response<Order?>(null, 400, "Pedido cancelado e não pode ser reembolsado");
            case EOrderStatus.Refunded:
                return new Response<Order?>(null, 400, "Pedido já reembolsado");
            case EOrderStatus.WaitingPayment:
                return new Response<Order?>(null, 400, "Pedido não pago e não pode ser reembolsado");
            case EOrderStatus.Paid:
                break;
            default:
                return new Response<Order?>(null, 400, "Pedido não pode ser reembolsado");
        }

        order.Status = EOrderStatus.Refunded;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível reembolsar seu pedido");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} reembolsado com sucesso");
    }

    public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
    {
        try
        {
            var query = context.Orders
                .AsNoTracking()
                .Include(x=>x.Product)
                .Include(x=>x.Voucher)
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt);

            var orders = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Order>?>(orders, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Order>?>(null, 500, "Não foi possível consultar seus pedidos");
        }
    }

    public async Task<Response<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
    {
        try
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.Number == request.Number && x.UserId == request.UserId);

            return order is null ? new Response<Order?>(null, 404, "Pedido não encontrado")
                : new Response<Order?>(order);
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível recuperar o pedido");
        }
    }
}