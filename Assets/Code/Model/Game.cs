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

		public void Mark(int x, int y)
		{
			if (_board[x, y].HasValue)
				return;

			_board[x, y] = _currentMark;

			if (_currentMark == BoardMark.X)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));

			if (Rows.Any(row => row.All(mark => mark == BoardMark.X)))
				_events.OnNext(new XWinsEvent());

            _currentMark = _currentMark == BoardMark.X ? BoardMark.O : BoardMark.X;
        }

        private IEnumerable<IEnumerable<BoardMark?>> Rows
            => Enumerable.Range(0, 3).Select(y => Enumerable.Range(0, 3).Select(x => _board[x, y]));
    }
}