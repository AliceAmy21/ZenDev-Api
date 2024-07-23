using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class ChatroomEntity
    {
        [Key]
        public long ChatId {get;set;}
        public long GroupId {get;set;}
        [ForeignKey(nameof(GroupId))]
        public GroupEntity GroupEntity {get;set;}
        public List<ChatMessageBridgeEntity> ChatMessageBridges {get;set;} =[];
    }
}