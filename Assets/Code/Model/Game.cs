using System;
using UniRx;

namespace Assets.Code.Model
{
	public class Game
	{
		private readonly ISubject<GameEvent> _events = new Subject<GameEvent>();
		public IObservable<GameEvent> Events => _events;
		public void MarkX(int x, int y)
		{
			_events.OnNext(new XMarkedEvent(0, 0));
		}
	}
}