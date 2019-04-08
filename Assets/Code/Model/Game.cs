using System;
using UniRx;

namespace Assets.Code.Model
{
	public class Game
	{
		private readonly ISubject<GameEvent> _events = new Subject<GameEvent>();
		public IObservable<GameEvent> Events => _events;

		private readonly bool[,] _board = new bool[3, 3];

		private bool _isXTurn = true;

		public void Mark(int x, int y)
		{
			if (_board[x, y])
				return;

			_board[x, y] = true;

			if (_isXTurn)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));

			_isXTurn = !_isXTurn;
		}
	}
}