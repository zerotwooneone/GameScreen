using System;
using System.Collections.Generic;
using GameScreen.MobStat;

namespace GameScreen.Persistence
{
    public class PrimaryDatamodel
    {
        public PrimaryDatamodel(IEnumerable<MobDatamodel> mobs)
        {
            Mobs = mobs;
        }
        [Obsolete("This is only used for serialization")]
        public PrimaryDatamodel()
        {
        }

        public IEnumerable<MobDatamodel> Mobs { get; set; }
    }
}