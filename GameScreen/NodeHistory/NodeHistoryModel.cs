using System.Collections.Generic;

namespace GameScreen.NodeHistory
{
    public class NodeHistoryModel
    {
        public NodeHistoryModel(IEnumerable<HistoryNode> historyNodes)
        {
            HistoryNodes = historyNodes;
        }

        public IEnumerable<HistoryNode> HistoryNodes { get; }
    }
}