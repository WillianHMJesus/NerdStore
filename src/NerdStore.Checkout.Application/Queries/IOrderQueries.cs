using NerdStore.Checkout.Application.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartDTO> GetCartByClientAsync(Guid clientId);
        Task<IEnumerable<OrderDTO>> GetOrderByClient(Guid clientId);
    }
}
