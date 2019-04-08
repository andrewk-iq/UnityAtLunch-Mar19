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

	protected void Assert_EventObserved(GameEvent expected, int duplicates = 1)
	{
		_observer.Received(duplicates).OnNext(expected);
	}

	protected void Assert_EventsObserved(params GameEvent[] expectedEvents)
	{
		foreach (var gameEvent in expectedEvents)
		{
			_observer.Received().OnNext(gameEvent);
		}
	}

	protected void Assert_EventNotObserved(GameEvent prohibited)
	{
		_observer.DidNotReceive().OnNext(prohibited);
	}
}