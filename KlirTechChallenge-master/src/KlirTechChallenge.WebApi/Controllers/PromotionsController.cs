using KlirTechChallenge.Application.Promotions;
using KlirTechChallenge.WebApi.Controllers.Base;
using KlirTechChallenge.Application.Promotion.Promotions;
using KlirTechChallenge.Application.Promotion.ChangePromotion;
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
    [Route("api/Promotions")]
    [ApiController]
    public class PromotionsController : BaseController
    {
        public PromotionsController(
            IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet, Route("")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(typeof(IList<PromotionsViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPromotions()
        {
            var query = new ListPromotionsQuery();
            return await Response(query);
        }

        /// <summary>
        /// Change a Quote 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "CanSave")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePromotion([FromBody] ChangePromotionRequest request)
        {
            var command = new ChangePromotionCommand(request.PromotionId, request.Name, request.Active);
            return await Response(command);
        }

    }
}