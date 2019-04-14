using Assets.Code.Model;
using NSubstitute;
using NUnit.Framework;

public class GameTests : GameTestFixture
{
	[Test]
	public void XMarked([Range(0, 2)] int x, [Range(0, 2)] int y)
	{
		Act_Mark(x, y);

		Assert_EventObserved(new XMarkedEvent(x, y));
	}

	[Test]
	public void XMarkedThenOMarked([Range(0, 2)] int x1, [Range(0, 2)] int y1)
	{
		var x2 = (x1 + 1) % 3;
		var y2 = (y1 + 1) % 3;

		Act_Mark(x1, y1);
		Act_Mark(x2, y2);

		Assert_EventsObserved(
			new XMarkedEvent(x1, y1),
			new OMarkedEvent(x2, y2)
		);
	}

	[Test]
	public void MarkSameCellTwice()
	{
		Act_Mark(0, 0);
		Act_Mark(0, 0);

		Assert_EventNotObserved(Arg.Any<OMarkedEvent>());
	}

	[Test]
	public void XWinsHorizontal([Range(0, 2)] int y1)
	{
		var y2 = (y1 + 1) % 3;

		Act_Mark(0, y1); // X
		Act_Mark(0, y2); // O
		Act_Mark(1, y1); // X
		Act_Mark(1, y2); // O
		Act_Mark(2, y1); // X

		Assert_EventsObserved(
			new XMarkedEvent(0, y1),
			new OMarkedEvent(0, y2),
			new XMarkedEvent(1, y1),
			new OMarkedEvent(1, y2),
			new XMarkedEvent(2, y1),
			new XWonEvent()
		);
	}

	[Test]
	public void XBlockedHorizontal([Range(0, 2)] int y1)
	{
		var y2 = (y1 + 1) % 3;

		Act_Mark(0, y1); // X
		Act_Mark(0, y2); // O
		Act_Mark(1, y1); // X
		Act_Mark(2, y1); // O
		Act_Mark(1, y2); // X

		Assert_EventNotObserved(Arg.Any<XWonEvent>());
	}

	[Test]
	public void XWinsVertical([Range(0, 2)] int x1)
	{
		var x2 = (x1 + 1) % 3;

		Act_Mark(x1, 0); // X
		Act_Mark(x2, 0); // O
		Act_Mark(x1, 1); // X
		Act_Mark(x2, 1); // O
		Act_Mark(x1, 2); // X

		Assert_EventsObserved(
			new XMarkedEvent(x1, 0),
			new OMarkedEvent(x2, 0),
			new XMarkedEvent(x1, 1),
			new OMarkedEvent(x2, 1),
			new XMarkedEvent(x1, 2),
			new XWonEvent()
		);
	}

	[Test]
	public void XWinsDiagonalDown()
	{
		Act_Mark(0, 0); // X
		Act_Mark(1, 0); // O
		Act_Mark(1, 1); // X
		Act_Mark(2, 1); // O
		Act_Mark(2, 2); // X

		Assert_EventsObserved(
			new XMarkedEvent(0, 0),
			new OMarkedEvent(1, 0),
			new XMarkedEvent(1, 1),
			new OMarkedEvent(2, 1),
			new XMarkedEvent(2, 2),
			new XWonEvent()
		);
	}

	[Test]
	public void XWinsDiagonalUp()
	{
		Act_Mark(0, 2); // X
		Act_Mark(1, 0); // O
		Act_Mark(1, 1); // X
		Act_Mark(2, 1); // O
		Act_Mark(2, 0); // X

		Assert_EventsObserved(
			new XMarkedEvent(0, 2),
			new OMarkedEvent(1, 0),
			new XMarkedEvent(1, 1),
			new OMarkedEvent(2, 1),
			new XMarkedEvent(2, 0),
			new XWonEvent()
		);
	}
}