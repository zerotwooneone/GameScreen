using System;
using System.Collections.Generic;
using GameScreen.Primary;

namespace GameScreen.MobStat
{
    public class MobDatamodel : IPositionable, ISizable
    {
        [Obsolete("only used for serialization")]
        public MobDatamodel()
        {
        }
        public MobDatamodel(string name, IEnumerable<MobStatDatamodel> mobStats, double x, double y, double height, double width)
        {
            Name = name;
            MobStats = mobStats;
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }

        public string Name { get; }
        public IEnumerable<MobStatDatamodel> MobStats { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; }
        public double Height { get; }
    }
}