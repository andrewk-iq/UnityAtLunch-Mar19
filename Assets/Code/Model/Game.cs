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

		private BoardMark _nextTurn = BoardMark.X;

		public void Mark(int x, int y)
		{
			if (_board[x, y].HasValue)
				return;

			_board[x, y] = _nextTurn;
			
			if (_nextTurn == BoardMark.X)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));
			
			if (ContainsSequenceOfMark(Rows.Concat(Columns).Append(DiagonalDown).Append(DiagonalUp), _nextTurn))
			{
				if (_nextTurn == BoardMark.X)
					_events.OnNext(new XWonEvent());
				else
					_events.OnNext(new OWonEvent());
			}

			_nextTurn = _nextTurn == BoardMark.X ? BoardMark.Y : BoardMark.X;
		}

		private static bool ContainsSequenceOfMark(IEnumerable<IEnumerable<BoardMark?>> sequences, BoardMark expected)
			=> sequences.Any(sequence => sequence.All(mark => mark == expected));

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