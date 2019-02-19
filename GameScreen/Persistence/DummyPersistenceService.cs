using GameScreen.MobStat;

namespace GameScreen.Persistence
{
    public class DummyPersistenceService : IPersistenceService
    {
        public PrimaryDatamodel Get()
        {
            return new PrimaryDatamodel(new[]
            {
                new MobDatamodel("mob 1", 
                    new[]
                    {
                        new MobStatDatamodel
                        {
                            Name = "Stat 1",
                            Value = 1,
                            Pinned = false
                        },
                        new MobStatDatamodel
                        {
                            Name = "Pinned Stat",
                            Value = 1,
                            Pinned = true
                        }
                    },
                    100,
                    150,
                    100,
                    100)
            });
        }
    }
}