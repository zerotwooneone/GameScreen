using GameScreen.Primary;
using System;
using System.Collections.Generic;

namespace GameScreen.MobStat
{
    public class MobDatamodel : IPositionable, ISizable
    {
        [Obsolete("only used for serialization")]
        public MobDatamodel()
        {
        }
        public MobDatamodel(Guid id, string name, IEnumerable<MobStatDatamodel> mobStats, double x, double y, double height, double width)
        {
            Id = id;
            Name = name;
            MobStats = mobStats;
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }

        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<MobStatDatamodel> MobStats { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; }
        public double Height { get; }
    }
}