using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Migrations.configurations
{
    public class GroupConfig
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string GroupDescription { get; set; } = string.Empty;
        public string GroupIconUrl { get; set; } = string.Empty;
        public long ExerciseTypeId { get; set; }
        public long MemberCount { get; set; }
    }
}