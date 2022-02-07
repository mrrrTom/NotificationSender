using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotificationSender.Data;
using NotificationSender.Interfaces;
using NotificationSender.Model;

namespace NotificationSender.Controllers
{
    [ApiController]
    [Route("Notification")]
    public class NotificationController : Controller
    {
        private readonly ISenderResolver _senderResolver;
        private readonly NotificationSenderContext _context;

        public NotificationController(NotificationSenderContext context, ISenderResolver senderResolver)
        {
            _senderResolver = senderResolver;
            _context = context;
        }

        /// <summary>
        /// Get notification status
        /// </summary>
        /// <param name="id">Notification identifier</param>
        /// <returns>Notification status</returns>
        [HttpGet]
        [Route("/Status/{id}")]
        public NotificationStatus GetStatus(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            var allNot = _context.Notification.ToList();
            var notification = _context.Notification
                .FirstOrDefault(m => m.Id == id);
            if (notification == null)
            {
                return NotificationStatus.Undefined;
            }

            return notification.Status;
        }

        /// <summary>
        /// Create and send notification
        /// </summary>
        /// <param name="notification">Notification data</param>
        /// <returns>Notification id and delivery status</returns>
        [HttpPost]
        [Route("/Create")]
        public NotificationInfo Create(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Model state is invalid.");
            }

            var id = Guid.NewGuid().ToString();
            notification.Id = id;
            notification.Status = NotificationStatus.NotificationCreated;
            _ = _context.Add(notification);
            _ = _context.SaveChanges();
            var sender = _senderResolver.Resolve(notification);
            if (!sender.ValidateInput(notification, out var errorMessage))
            {
                throw new ArgumentException(errorMessage.TrimEnd());
            }

            if (sender.Send(notification)) 
            {
                notification.Status = NotificationStatus.DeliverySucceed;
            }
            else
            {
                notification.Status = NotificationStatus.DeliveryFailed;
            }

            _ = _context.Update(notification);
            _ = _context.SaveChanges();
            return new NotificationInfo(notification);
        }
    }
}
