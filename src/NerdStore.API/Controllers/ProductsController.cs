using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductAppService _productAppService;

        public ProductsController(IProductAppService productAppService,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, mediator)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var products = await _productAppService.GetAllAsync();

                if (!products.Any())
                    return NoContent();

                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet("{categoryCode}/Category")]
        public async Task<IActionResult> GetByCategoryAsync(int categoryCode)
        {
            try
            {
                var products = await _productAppService.GetByCategoryAsync(categoryCode);

                if (!products.Any()) 
                    return NoContent();

                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productAppService.GetByIdAsync(id);

                if (product == null)
                    return NoContent();

                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productAppService.AddAsync(model);
                    return Ok();
                }

                return BadRequest(new { Errors = GetModelStateErrors() });
            }
            catch (Exception exception)
            {
                return ResponseException(exception);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productAppService.UpdateAsync(model);
                    return Ok();
                }

                return BadRequest(new { Errors = GetModelStateErrors() });
            }
            catch (Exception exception)
            {
                return ResponseException(exception);
            }
        }
    }
}
