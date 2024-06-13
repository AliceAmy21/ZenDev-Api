using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenDev.Common.Helpers
{
    public class GroupInvitationQueryObject
    {
        public string? searchQuery { get; set; } = null;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
