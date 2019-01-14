using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("all")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task<IEnumerable<OrderDto>> GetAll([FromQuery] PaginationDto pagination)
        {
            return _ordersService.GetOrders(pagination, HttpContext.RequestAborted);
        }

        [HttpGet]
        [ApiAuthorize(ApiRoles.User)]
        public Task<IEnumerable<OrderDto>> GetPersonOrders([FromQuery] PaginationDto pagination)
        {
            return _ordersService.GetPersonOrders(GetPersonId(), pagination, HttpContext.RequestAborted);
        }

        [HttpGet("{orderId}")]
        [ApiAuthorize(ApiRoles.User)]
        public Task<OrderDto> GetOrder(int orderId)
        {
            return _ordersService.GetOrder(orderId, HttpContext.RequestAborted);
        }

        [HttpPost]
        [ApiAuthorize(ApiRoles.User)]
        public Task AddOrder(NewOrderDto order)
        {
            order.PersonId = GetPersonId();
            return _ordersService.AddOrder(order, HttpContext.RequestAborted);
        }

        private int GetPersonId()
        {
            int personId;
            if (!int.TryParse(User.FindFirst(ClaimsIdentityConsts.PersonIdClaimType).Value, out personId))
            {
                throw new UnauthorizedAccessException("User is not determinated");
            }

            return personId;
        }
    }
}