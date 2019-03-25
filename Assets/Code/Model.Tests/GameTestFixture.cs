using System;
using Assets.Code.Model;
using NSubstitute;

public class GameTestFixture
{
	private readonly Game _game = new Game();
	private IObserver<GameEvent> _observer = Substitute.For<IObserver<GameEvent>>();

	protected void Act_MarkX(int x, int y)
	{
		_game.MarkX(0, 0);
	}

	protected void Assert_EventObserved(GameEvent gameEvent)
	{
		_observer.Received().OnNext(gameEvent);
	}
}