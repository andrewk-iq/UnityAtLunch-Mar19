using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Code.Model
{
	public class Game
	{
		private readonly ISubject<GameEvent> _events = new Subject<GameEvent>();
		public IObservable<GameEvent> Events => _events;

		private readonly BoardMark?[,] _board = new BoardMark?[3, 3];

		private BoardMark _currentTurn = BoardMark.X;

		public void Mark(int x, int y)
		{
			if (_board[x, y].HasValue)
				return;

			_board[x, y] = _currentTurn;
			
			if (_currentTurn == BoardMark.X)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));
			
			if (ContainsSequenceOfMark(_currentTurn))
			{
				if (_currentTurn == BoardMark.X)
					_events.OnNext(new XWonEvent());
				else
					_events.OnNext(new OWonEvent());
			}

			_currentTurn = _currentTurn == BoardMark.X ? BoardMark.Y : BoardMark.X;
		}

		private bool ContainsSequenceOfMark(BoardMark expected)
			=> Sequences.Any(sequence => sequence.All(mark => mark == expected));

		private IEnumerable<IEnumerable<BoardMark?>> Sequences 
			=> Rows.Concat(Columns).Append(DiagonalDown).Append(DiagonalUp);
		
		private IEnumerable<IEnumerable<BoardMark?>> Rows
			=> Enumerable.Range(0, 3).Select(y => Enumerable.Range(0, 3).Select(x => _board[x, y]));

		private IEnumerable<IEnumerable<BoardMark?>> Columns
			=> Enumerable.Range(0, 3).Select(x => Enumerable.Range(0, 3).Select(y => _board[x, y]));

		private IEnumerable<BoardMark?> DiagonalDown
			=> Enumerable.Range(0, 3).Select(x => _board[x, x]);

		private IEnumerable<BoardMark?> DiagonalUp
			=> Enumerable.Range(0, 3).Select(x => _board[x, 2 - x]);
	}
}