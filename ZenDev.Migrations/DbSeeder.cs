using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.Migrations
{
    public class DbSeeder
    {
        public ZenDevDbContext _dbContext;
        public ExerciseEntity[] _exercises;
        public ExerciseTypeEntity[] _exerciseTypes;
        public AchievementEntity[] _achievements;


        public DbSeeder(ZenDevDbContext dbContext)
        {
            _dbContext = dbContext;
            _exercises = [];
            _exerciseTypes = [];
            _achievements = [];
        }

        public void SeedData()
        {
            AddExercises();
            AddExerciseTypes();
            AddAchievements();
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

        private void AddAchievements()
        {
            var jsonData = ReadJSON<AchievementConfig>(Constants.ACHIEVEMENTS_FILE_NAME);
            
            var achievementsLength = jsonData.Length;
            _achievements = new AchievementEntity[achievementsLength];

            for (int i = 0; i < achievementsLength; i++)
            {
                _achievements[i] = new AchievementEntity
                {
                    AchievementName = jsonData[i].AchievementName,
                    AchievementDescription = jsonData[i].AchievementDescription,
                    AchievementIcon = jsonData[i].AchievementIcon,
                };
            }

            if (!_dbContext.Achievements.Any())
            {
                _dbContext.Achievements.AddRange(_achievements);
                _dbContext.SaveChanges();
            }
        }
    }
}