using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationSender.Model
{
    public class Notification
    {
        public string Id { get; set; }
        public NotificationStatus Status { get; set; }
        /// <summary>
        /// Notification target device type
        /// </summary>
        [Required(ErrorMessage = "Target device type is required")]
        public NotificationType Type { get; set; }

        #region iOS
        /// <summary>
        /// Device identifier, must contain less, than 50 symbols
        /// </summary>
        public string PushToken { get; set; }
        /// <summary>
        /// Message text, must contain less, than 2000 symbols
        /// </summary>
        public string Alert { get; set; }
        public int Priority { get; set; } = 10;
        public bool IsBackground { get; set; } = true;
        #endregion

        #region Android
        /// <summary>
        /// Device identifier, must contain less, than 50 symbols
        /// </summary>
        public string DeviceToken { get; set; }
        /// <summary>
        /// Message text, must contain less, than 2000 symbols
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Must contain less, than 255 symbols
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Optional fiels, must contain 2000 symbols
        /// </summary>
        public string Condition { get; set; }
        #endregion
    }
}
