using System.Collections.Generic;
using System.Text;

namespace src
{
    class Board<T>
    {
        LinkedList<T> _board = new LinkedList<T>();
        LinkedListNode<T> _currentNode;

        public Board()
        {
            _board.AddFirst(default(T));
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

        public void AddClockwiseToCurrent(T value)
        {
            _board.AddAfter(_currentNode, value);
        }

        public void AddClockwiseToCurrentAndSelect(T value)
        {
            AddClockwiseToCurrent(value);
            MoveClockwise(1);
        }

        public T RemoveCounterClockwise()
        {
            LinkedListNode<T> _nodeToBeRemoved = _currentNode.Previous;
            T valueOfRemovedNode = _nodeToBeRemoved.Value;
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
            LinkedListNode<T> current = _board.First;
            while(current != null){                
                string toAppend = current == _currentNode ? $"({current.Value}) " : $"{current.Value} ";
                stringBuilder.Append(toAppend);

                current = current.Next;
            }
            return stringBuilder.ToString();
        } 
    }
}