using System.ComponentModel.DataAnnotations;

namespace ZenDev.Persistence.Entities
{
    public class ReactionIconEntity
    {
        [Key]
        public long ReactionIconId {get;set;}
        public string ReactionIconUrl {get;set;} = String.Empty;
        public List<ReactionMessageBridgeEntity> ReactionMessageBridgeEntities {get;set;} = [];
    }
}