using System;
using UniRx;

namespace Assets.Code.Model
{
	public class Game
	{
		private readonly ISubject<GameEvent> _events = new Subject<GameEvent>();
		public IObservable<GameEvent> Events => _events;

		private bool _isXTurn = true;

		public void Mark(int x, int y)
		{
			if (_isXTurn)
				_events.OnNext(new XMarkedEvent(x, y));
			else
				_events.OnNext(new OMarkedEvent(x, y));

			_isXTurn = !_isXTurn;
		}
	}
}