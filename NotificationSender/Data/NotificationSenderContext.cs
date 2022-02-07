using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationSender.Model;

namespace NotificationSender.Data
{
    public class NotificationSenderContext : DbContext
    {
        public NotificationSenderContext (DbContextOptions<NotificationSenderContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<NotificationSender.Model.Notification> Notification { get; set; }
    }
}
