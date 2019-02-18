using GameScreen.MobStat;

namespace GameScreen.Persistence
{
    public class DummyPersistenceService : IPersistenceService
    {
        public PrimaryDatamodel Get()
        {
            return new PrimaryDatamodel(new[]
            {
                new MobDatamodel("mob 1", new[]
                    {
                        new MobStatDatamodel
                        {
                            Name = "Stat 1",
                            Value = 1,
                            Pinned = false
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