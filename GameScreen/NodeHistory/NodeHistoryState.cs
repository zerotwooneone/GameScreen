using System;
using System.Collections.Generic;
using System.Linq;

namespace GameScreen.NodeHistory
{
    public class NodeHistoryState
    {
        private readonly int _maxHistoryNodes;
        public virtual HistoryNode Current { get; }
        public virtual IEnumerable<HistoryNode> ForwardNodes { get; }
        public virtual IEnumerable<HistoryNode> BackNodes { get; }

        public NodeHistoryState(HistoryNode current, 
            int maxHistoryNodes,
            IEnumerable<HistoryNode> backNodes = null, 
            IEnumerable<HistoryNode> forwardNodes = null)
        {
            if(maxHistoryNodes < 1) throw new ArgumentException($"{nameof(maxHistoryNodes)} must be positive", nameof(maxHistoryNodes));
            _maxHistoryNodes = maxHistoryNodes;

            Current = current ?? throw new ArgumentNullException(nameof(current));
            BackNodes = backNodes?.ToArray() ?? Enumerable.Empty<HistoryNode>();
            ForwardNodes = forwardNodes?.ToArray() ?? Enumerable.Empty<HistoryNode>();
        }

        public virtual NodeHistoryState Append(HistoryNode current)
        {
            if(current == null) throw new ArgumentNullException(nameof(current));

            var historyNodes = BackNodes.Append(Current).ToArray();
            var backNodes = historyNodes.TakeLast(_maxHistoryNodes);
            return new NodeHistoryState(current, _maxHistoryNodes, backNodes,  forwardNodes: null);
        }

        public virtual NodeHistoryState GoBack()
        {
            var current = BackNodes.Skip(BackNodes.Count() - 1).First();
            var forwardNodes = ForwardNodes.Append(Current);
            var newBackNodes = BackNodes.Take(BackNodes.Count() - 1);
            return new NodeHistoryState(current, _maxHistoryNodes, newBackNodes, forwardNodes);
        }

        public virtual NodeHistoryState GoForward()
        {
            var current = ForwardNodes.Take(1).First();
            var newForwardNodes = ForwardNodes.Skip(1);
            var backNodes = BackNodes.Append(Current);
            return new NodeHistoryState(current, _maxHistoryNodes, backNodes, newForwardNodes);
        }

        public virtual NodeHistoryState GoBackTo(HistoryNode historyNode)
        {
            while (true)
            {
                var result = GoBack();
                if (historyNode.Equals(result.Current))
                {
                    return result;
                }
            }
        }
    }
}