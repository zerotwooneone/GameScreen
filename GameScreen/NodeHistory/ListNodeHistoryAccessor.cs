using System.Collections.Generic;

namespace GameScreen.NodeHistory
{
    public class ListNodeHistoryAccessor : INodeHistoryAccessor
    {
        private readonly List<HistoryNode> _list;
        private readonly NodeHistoryModel _historyModel;

        public ListNodeHistoryAccessor()
        {
            _list = new List<HistoryNode>();
            _historyModel = new NodeHistoryModel(_list);
        }
        public NodeHistoryModel Get()
        {
            return _historyModel;
        }
    }
}