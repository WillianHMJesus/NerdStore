using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        protected Guid ClientId = Guid.Parse("78dca47e-d4e1-4d2c-8885-0ece580df1e8");

        protected BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected bool OperationIsValid()
        {
            return !_notifications.HasNotifications();
        }

        protected List<string> GetNotificationsErrors()
        {
            var messages = new List<string>();
            _notifications.GetNotifications().ForEach(x => messages.Add(x.Value));

            return messages;
        }

        protected List<string> GetModelStateErrors()
        {
            var messages = new List<string>();
            foreach (var erro in ModelState.Values.SelectMany(x => x.Errors))
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                messages.Add(errorMessage);
            }

            return messages;
        }

        protected IActionResult ResponseException(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(DomainException):
                    var domainException = (DomainException)exception;
                    return BadRequest(new { Errors = new List<string> { domainException.GetErrorMessage() } });
                default:
                    return StatusCode(500, exception);
            }
        }
    }
}
