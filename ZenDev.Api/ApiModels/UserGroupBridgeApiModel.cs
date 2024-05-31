using ZenDev.Persistence.Entities;

namespace ZenDev.Api.ApiModels
{
    public class UserGroupBridgeApiModel
    {  
        public long UserGroupId { get; set; }

        public bool GroupAdmin { get; set; }

        public long UserId { get; set; }

        public long GroupId { get; set; }
    }
}