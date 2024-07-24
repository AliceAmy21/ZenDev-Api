using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Migrations.configurations
{
    public class UGBridgeConfig
    {
        public long UserGroupId { get; set; }

        public bool GroupAdmin { get; set; }

        public long UserId { get; set; }

        public long GroupId { get; set; }

        public long Points{ get; set; }
    }
}