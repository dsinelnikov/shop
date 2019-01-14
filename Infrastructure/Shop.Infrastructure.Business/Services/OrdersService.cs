using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Infrastructure.Data;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using Shop.Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrder(NewOrderDto order, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productIds = order.Items.Select(item => item.ProductId).ToList();
            var products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var person = new Person { Id = order.PersonId };
            _dbContext.Attach(person);

            var newOrder = new Order
            {
                Created = DateTime.Now,
                Person = person
            };
            _dbContext.Attach(newOrder);

            var items = order.Items.Select(i => new OrderItem {
                Count = i.Count,
                Price = products[i.ProductId].Price,
                Product = products[i.ProductId],
                Order = newOrder
            }).ToList();
            _dbContext.AttachRange(items);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderDto> GetOrder(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var order = await _dbContext.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Created = o.Created,
                    Items = o.Items.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        Price = oi.Price,
                        Count = oi.Count,
                        Product = new ProductDto
                        {
                            Id = oi.Product.Id,
                            Name = oi.Product.Name,
                            Price = oi.Product.Price
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            if(order == null)
            {
                throw new ItemNotFoundException(id, "Order");
            }

            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Orders
                .Skip(pagination.Offset)
                .Take(pagination.Count)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Created = o.Created,
                    Items = o.Items.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        Price = oi.Price,
                        Count = oi.Count,
                        Product = new ProductDto
                        {
                            Id = oi.Product.Id,
                            Name = oi.Product.Name,
                            Price = oi.Product.Price
                        }
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<OrderDto>> GetPersonOrders(int personId, PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Orders
                .Skip(pagination.Offset)
                .Take(pagination.Count)
                .Where(o => o.Person.Id == personId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Created = o.Created,
                    Items = o.Items.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        Price = oi.Price,
                        Count = oi.Count,
                        Product = new ProductDto
                        {
                            Id = oi.Product.Id,
                            Name = oi.Product.Name,
                            Price = oi.Product.Price
                        }
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }
    }
}
