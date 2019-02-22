using GameScreen.MobStat;
using System;

namespace GameScreen.Persistence
{
    public class DummyPersistenceService : IPersistenceService
    {
        public PrimaryDatamodel Get()
        {
            return new PrimaryDatamodel(new[]
            {
                new MobDatamodel(Guid.Parse("221a84d1-29ea-44d8-b2c7-d2edc854795c"),
                    "mob 1",
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
                    200,
                    400)
            });
        }
    }
}