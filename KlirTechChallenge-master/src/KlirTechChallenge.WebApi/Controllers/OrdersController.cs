using KlirTechChallenge.WebApi.Controllers.Base;
using KlirTechChallenge.Application.Orders.GetOrders;
using KlirTechChallenge.Application.Orders.PlaceOrder;
using KlirTechChallenge.Application.Orders.GetOrderDetails;
using KlirTechChallenge.Application.Orders.ListOrderStoredEvents;
using KlirTechChallenge.Application.Core.EventSourcing.StoredEventsData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using MediatR;
using System.Collections.Generic;

namespace KlirTechChallenge.WebApi.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseController
    {
        public OrdersController(
            IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Returns the orders placed by a given cursomer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet, Route("{customerId:guid}")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(typeof(List<OrderDetailsViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrders([FromRoute] Guid customerId)
        {
            var query = new GetOrdersQuery(customerId);
            return await Response(query);
        }

        /// <summary>
        /// Returns the details of a given order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet, Route("{customerId:guid}/{orderId:guid}/details")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(typeof(OrderDetailsViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderDetails([FromRoute] Guid customerId, [FromRoute] Guid orderId)
        {
            var query = new GetOrderDetailsQuery(customerId, orderId);
            return await Response(query);
        }

        /// <summary>
        /// Place an order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("{quoteId:guid}")]
        [Authorize(Policy = "CanSave")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid quoteId, [FromBody] PlaceOrderRequest request)
        {
            var command = new PlaceOrderCommand(quoteId, request.CustomerId, request.Currency);
            return await Response(command);
        }

        /// <summary>
        /// Returns the Stored Events of a given Order
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet, Route("{orderId:guid}/events")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(typeof(IList<StoredEventData>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListEvents([FromRoute] Guid orderId)
        {
            var query = new ListOrderStoredEventsQuery(orderId);
            return await Response(query);
        }
    }
}