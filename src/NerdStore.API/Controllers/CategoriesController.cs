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
    public class CategoriesController : BaseController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ICategoryAppService categoryAppService,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, mediator)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var categories = await _categoryAppService.GetAllAsync();

                if (!categories.Any())
                    return NoContent();

                return Ok(categories);
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
                var category = await _categoryAppService.GetByIdAsync(id);

                if (category == null)
                    return NoContent();

                return Ok(category);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryAppService.AddAsync(model);
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
        public async Task<IActionResult> PutAsync([FromBody] CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryAppService.UpdateAsync(model);
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
