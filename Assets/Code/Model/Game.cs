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

		private BoardMark _currentMark;

		private bool _gameWon;

		public void Mark(int x, int y)
		{
			if (_gameWon || _board[x, y].HasValue)
				return;

			_board[x, y] = _currentMark;

			if (_currentMark == BoardMark.X)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));

			if (AnySequenceAllExpectedMark(_currentMark))
			{
				_gameWon = true;

				if (_currentMark == BoardMark.X)
					_events.OnNext(new XWinsEvent());
				else
					_events.OnNext(new OWinsEvent());
			}

			_currentMark = _currentMark == BoardMark.X ? BoardMark.O : BoardMark.X;
		}

		private bool AnySequenceAllExpectedMark(BoardMark expected) 
			=> Sequences.Any(sequence => sequence.All(mark => mark == expected));

		private IEnumerable<IEnumerable<BoardMark?>> Sequences
			=> Rows.Concat(Columns).Append(DownDiagonal).Append(UpDiagonal);

		private IEnumerable<IEnumerable<BoardMark?>> Rows
			=> Enumerable.Range(0, 3).Select(y => Enumerable.Range(0, 3).Select(x => _board[x, y]));

		private IEnumerable<IEnumerable<BoardMark?>> Columns
			=> Enumerable.Range(0, 3).Select(x => Enumerable.Range(0, 3).Select(y => _board[x, y]));

		private IEnumerable<BoardMark?> DownDiagonal
			=> Enumerable.Range(0, 3).Select(x => _board[x, x]);

		private IEnumerable<BoardMark?> UpDiagonal
			=> Enumerable.Range(0, 3).Select(x => _board[x, 2 - x]);
	}
}