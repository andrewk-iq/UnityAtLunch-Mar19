using System;
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
			
			if (_board[0, 0] == BoardMark.X && _board[1, 0] == BoardMark.X && _board[2, 0] == BoardMark.X)
				_events.OnNext(new XWonEvent());

			_nextTurn = _nextTurn == BoardMark.X ? BoardMark.Y : BoardMark.X;
		}
	}
}