using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenDev.BusinessLogic.Models
{
    public class NotificationModel
    {
        public long InvitedUserId { get; set; }

        public string InvitedUserName { get; set; }

        public long InviteSenderId { get; set; }

        public string InviteSenderUserName { get; set; }

        public long groupId { get; set; }

        public string groupName { get; set; }

        public string InviteSenderAvatarUrlIcon { get; set; }
    }
}
