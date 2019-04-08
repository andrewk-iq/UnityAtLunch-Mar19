using System;
using Assets.Code.Model;
using NSubstitute;
using NUnit.Framework;

public class GameTestFixture
{
	private  Game _game;
	private IObserver<GameEvent> _observer;

	[SetUp]
	public void Setup()
	{
		_game = new Game();
		_observer = Substitute.For<IObserver<GameEvent>>();
		_game.Events.Subscribe(_observer);
	}

	protected void Act_Mark(int x, int y)
	{
		_game.Mark(x, y);
	}

	protected void Assert_EventObserved(GameEvent gameEvent)
	{
		_observer.Received().OnNext(gameEvent);
	}

	protected void Assert_EventsObserved(params GameEvent[] gameEvents)
	{
		foreach (var gameEvent in gameEvents)
		{
			_observer.Received().OnNext(gameEvent);
		}
	}
}