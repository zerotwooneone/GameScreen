using System;

namespace GameScreen.NodeHistory
{
    public class HistoryNode
    {
        public HistoryNode(string nodeName, string locationId)
        {
            if (string.IsNullOrWhiteSpace(nodeName))
                throw new ArgumentException($"{nameof(nodeName)} can not be blank", nameof(nodeName));
            if(string.IsNullOrWhiteSpace(locationId))
                throw new ArgumentException($"{nameof(locationId)} can not be blank", nameof(locationId));
            NodeName = nodeName;
            LocationId = locationId;
        }

        public string NodeName {get;}
        public string LocationId { get; }
    }
}