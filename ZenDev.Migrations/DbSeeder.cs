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


        public DbSeeder(ZenDevDbContext dbContext)
        {
            _dbContext = dbContext;
            _exercises = [];
        }

        public void SeedData()
        {
            AddExercises();
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
    }
}