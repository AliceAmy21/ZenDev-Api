using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ZenDev.Migrations.configurations;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.Migrations
{
    public class DbSeeder
    {
        public ZenDevDbContext _dbContext;
        public ExerciseEntity[] _exercises;
        public ExerciseTypeEntity[] _exerciseTypes;
        public UserEntity[] _users;
        public GroupEntity[] _groups;
        public UserGroupBridgeEntity[] _ugBridge;
        public ChallengeEntity[] _challenges;
        public UserChallengeBridgeEntity[] _ucBridge;



        public DbSeeder(ZenDevDbContext dbContext)
        {
            _dbContext = dbContext;
            _exercises = [];
            _exerciseTypes = [];
            _users =[];
            _ugBridge = [];
            _ucBridge = [];
            _challenges = [];
            _groups = [];

        }

        public void SeedData()
        {
            AddExercises();
            AddExerciseTypes();
            AddUsers();
            AddGroups();
            AddBridgeUG();
            AddChallenges();
            AddBridgeUC();
        }

        public TEntity[] ReadJSON<TEntity>(string fileName) 
        where TEntity : class
        {
            var result = Array.Empty<TEntity>();

            using (StreamReader r = new(Path.Combine(Constants.SEEDING_DATA_DIRECTORY, fileName)))
            {
                string json = r.ReadToEnd();

                if (json.IsNullOrEmpty()) return [];

                result = JsonConvert.DeserializeObject<TEntity[]>(json);
            }
            return result;
        }

        private void AddExercises()
        {
            var jsonData = ReadJSON<ExerciseConfig>(Constants.EXERCISES_FILE_NAME);
            
            var exercisesLength = jsonData.Length;
            _exercises = new ExerciseEntity[exercisesLength];

            for (int i = 0; i < exercisesLength; i++)
            {
                _exercises[i] = new ExerciseEntity
                {
                    ExerciseName = jsonData[i].ExerciseName
                };
            }

            if (!_dbContext.Exercises.Any())
            {
                _dbContext.Exercises.AddRange(_exercises);
                _dbContext.SaveChanges();
            }
        }

        private void AddExerciseTypes()
        {
            var jsonData = ReadJSON<ExerciseTypeConfig>(Constants.EXERCISETYPES_FILE_NAME);
            
            var exerciseTypesLength = jsonData.Length;
            _exerciseTypes = new ExerciseTypeEntity[exerciseTypesLength];

            for (int i = 0; i < exerciseTypesLength; i++)
            {
                _exerciseTypes[i] = new ExerciseTypeEntity
                {
                    ExerciseType = jsonData[i].ExerciseType
                };
            }

            if (!_dbContext.ExerciseTypes.Any())
            {
                _dbContext.ExerciseTypes.AddRange(_exerciseTypes);
                _dbContext.SaveChanges();
            }
        }

        private void AddUsers()
        {
            var jsonData = ReadJSON<UserConfig>(Constants.USER_FILE_NAME);
            
            var usersLength = jsonData.Length;
            _users = new UserEntity[usersLength];

            for (int i = 0; i < usersLength; i++)
            {
                _users[i] = new UserEntity
                {
                    UserName = jsonData[i].UserName,
                    UserEmail = jsonData[i].UserEmail,
                    StravaRefreshToken = jsonData[i].StravaRefreshToken,
                    Streak = jsonData[i].Streak,
                    AvatarIconUrl = jsonData[i].AvatarIconUrl,
                    LastActive = jsonData[i].LastActive,
                    TotalPoints = jsonData[i].TotalPoints
                };
            }

            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(_users);
                _dbContext.SaveChanges();
            }
        }

        private void AddGroups()
        {
            var jsonData = ReadJSON<GroupConfig>(Constants.GROUPS_FILE_NAME);
            
            var groupLength = jsonData.Length;
            _groups = new GroupEntity[groupLength];

            for (int i = 0; i < groupLength; i++)
            {
                _groups[i] = new GroupEntity
                {
                    GroupName = jsonData[i].GroupName,
                    GroupDescription = jsonData[i].GroupDescription,
                    GroupIconUrl = jsonData[i].GroupIconUrl,
                    ExerciseTypeId = jsonData[i].ExerciseTypeId,
                    MemberCount = jsonData[i].MemberCount
                };
            }

            if (!_dbContext.Groups.Any())
            {
                _dbContext.Groups.AddRange(_groups);
                _dbContext.SaveChanges();
            }
        }

        private void AddBridgeUG()
        {
            var jsonData = ReadJSON<UGBridgeConfig>(Constants.UGBRIDGE_FILE_NAME);
            
            var ugLength = jsonData.Length;
            _ugBridge = new UserGroupBridgeEntity[ugLength];

            for (int i = 0; i < ugLength; i++)
            {
                _ugBridge[i] = new UserGroupBridgeEntity
                {
                    UserId = jsonData[i].UserId,
                    GroupId = jsonData[i].GroupId,
                    GroupAdmin = jsonData[i].GroupAdmin
                };
            }

            if (!_dbContext.UserGroupBridge.Any())
            {
                _dbContext.UserGroupBridge.AddRange(_ugBridge);
                _dbContext.SaveChanges();
            }
        }

        private void AddBridgeUC()
        {
            var jsonData = ReadJSON<UCBridgeConfig>(Constants.UCBRIDGE_FILE_NAME);
            
            var ucLength = jsonData.Length;
            _ucBridge = new UserChallengeBridgeEntity[ucLength];

            for (int i = 0; i < ucLength; i++)
            {
                _ucBridge[i] = new UserChallengeBridgeEntity
                {
                    UserId = jsonData[i].UserId,
                    ChallengeId = jsonData[i].ChallengeId,
                    AmountCompleted = jsonData[i].AmountCompleted
                };
            }

            if (!_dbContext.UserChallengeBridge.Any())
            {
                _dbContext.UserChallengeBridge.AddRange(_ucBridge);
                _dbContext.SaveChanges();
            }
        }

        private void AddChallenges()
        {
            var jsonData = ReadJSON<ChallengeConfig>(Constants.CHALLENGES_FILE_NAME);
            
            var cLength = jsonData.Length;
            _challenges = new ChallengeEntity[cLength];

            for (int i = 0; i < cLength; i++)
            {
                _challenges[i] = new ChallengeEntity
                {
                    ChallengeName = jsonData[i].ChallengeName,
                    ChallengeDescription = jsonData[i].ChallengeDescription,
                    ChallengeStartDate = jsonData[i].ChallengeStartDate,
                    ChallengeEndDate = jsonData[i].ChallengeEndDate,
                    Measurement = jsonData[i].Measurement,
                    AmountToComplete = jsonData[i].AmountToComplete,
                    ExerciseId = jsonData[i].ExerciseId,
                    GroupId = jsonData[i].GroupId,
                    Admin = jsonData[i].Admin 
                };
            }

            if (!_dbContext.Challenges.Any())
            {
                _dbContext.Challenges.AddRange(_challenges);
                _dbContext.SaveChanges();
            }
        }
    }
}