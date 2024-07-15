using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZenDev.Persistence.Entities
{
    public class TournamentEntity
    {
        [Key]
        public long TournamentId {get;set;}
        [MaxLength(100)]
        public string TournamentName {get;set;} = string.Empty;
        [MaxLength(250)]
        public string TournamentDescription {get;set;} = string.Empty;
        public long ExerciseId {get;set;}
        [ForeignKey(nameof(ExerciseId))]
        public ExerciseEntity ExerciseEntity {get;set;}
        public DateTimeOffset StartDate {get;set;}
        public DateTimeOffset EndDate {get;set;}
        public List<TournamentGroupBridgeEntity> TournamentGroupBridgeEntities {get;set;} = [];
    }
}