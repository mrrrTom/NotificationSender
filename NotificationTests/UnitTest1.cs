using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using NotificationSender.Controllers;
using NotificationSender.Data;
using NotificationSender.Interfaces;
using NotificationSender.Model;
using NotificationSender.Services;

using Xunit;

namespace NotificationTests
{
    public class UnitTest1
    {
        private readonly NotificationController _controller;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<NotificationSenderContext>()
                                .UseInMemoryDatabase(databaseName: "NotificationDB")
                                .Options;
            var ctx = new NotificationSenderContext(options);
            var androidLogger = new Mock<ILogger<AndroidNotificationService>>();
            var androidService = new AndroidNotificationService(androidLogger.Object);
            var iosLogger = new Mock<ILogger<IOSNotificationService>>();
            var iosdService = new IOSNotificationService(iosLogger.Object);
            var resolver = new SenderResolver(androidService, iosdService);
            _controller = new NotificationController(ctx, resolver);
        }

        [Fact]
        public void AndroidNotificationTest()
        {
            var n = new Notification
            {
                Type = NotificationType.AndroidNotification,
                DeviceToken = "Device token string",
                Message = "Message string",
                Title = "Title string",
                Condition = "Condition string"
            };
            var notificationInfo = _controller.Create(n);
            var notificationId = notificationInfo.Id;
            var notificationStatus = notificationInfo.Status;

            Assert.Equal(notificationStatus, _controller.GetStatus(notificationId));
        }

        [Fact]
        public void IOSNotificationTest()
        {
            var n = new Notification
            {
                Type = NotificationType.IOSNotification,
                PushToken = "Device token string",
                Alert = "Message string",
                Priority = 2,
                IsBackground = false
            };
            var notificationInfo = _controller.Create(n);
            var notificationId = notificationInfo.Id;
            var notificationStatus = notificationInfo.Status;

            Assert.Equal(notificationStatus, _controller.GetStatus(notificationId));
        }
    }
}
