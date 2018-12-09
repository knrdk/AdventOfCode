using System.Collections.Generic;
using System.Text;

namespace src
{
    class Board
    {
        LinkedList<int> _board = new LinkedList<int>();
        LinkedListNode<int> _currentNode;

        public Board()
        {
            _board.AddFirst(0);
            _currentNode = _board.First;
        }

        public void MoveCurrent(int shift)
        {
            if (shift > 0)
            {
                MoveClockwise(shift);
            }
            else
            {
                MoveCounterClockwise(-1 * shift);
            }
        }

        public void AddClockwiseToCurrent(int value)
        {
            _board.AddAfter(_currentNode, value);
        }

        public void AddClockwiseToCurrentAndSelect(int value)
        {
            AddClockwiseToCurrent(value);
            MoveClockwise(1);
        }

        public int RemoveCounterClockwise()
        {
            LinkedListNode<int> _nodeToBeRemoved = _currentNode.Previous;
            int valueOfRemovedNode = _nodeToBeRemoved.Value;
            _board.Remove(_nodeToBeRemoved);
            return valueOfRemovedNode;
        }

        private void MoveClockwise(int shift)
        {
            for (int i = 0; i < shift; i++)
            {
                _currentNode = _currentNode.Next ?? _board.First;
            }
        }

        private void MoveCounterClockwise(int shift)
        {
            for (int i = 0; i < shift; i++)
            {
                _currentNode = _currentNode.Previous ?? _board.Last;
            }
        }

        public override string ToString(){
            var stringBuilder = new StringBuilder();
            LinkedListNode<int> current = _board.First;
            while(current != null){                
                string toAppend = current == _currentNode ? $"({current.Value}) " : $"{current.Value} ";
                stringBuilder.Append(toAppend);

                current = current.Next;
            }
            return stringBuilder.ToString();
        } 
    }
}