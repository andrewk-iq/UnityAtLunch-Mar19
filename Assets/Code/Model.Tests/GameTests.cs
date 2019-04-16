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
    public void XWinsHorizontal([Range(0, 2)] int y0)
    {
        var y1 = (y0 + 1) % 3;

        Act_Mark(0, y0); // X
        Act_Mark(0, y1); // O
        Act_Mark(1, y0); // X
        Act_Mark(1, y1); // O
        Act_Mark(2, y0); // X

        Assert_EventsObserved(
            new XMarkedEvent(0, y0),
            new OMarkedEvent(0, y1),
            new XMarkedEvent(1, y0),
            new OMarkedEvent(1, y1),
            new XMarkedEvent(2, y0),
            new XWinsEvent()
        );
    }

    [Test]
    public void OWinsHorizontal([Range(0, 2)] int y0)
    {
        var y1 = (y0 + 1) % 3;
        var y2 = (y0 + 2) % 3;

        Act_Mark(0, y2); // X
        Act_Mark(0, y0); // O
        Act_Mark(1, y1); // X
        Act_Mark(1, y0); // O
        Act_Mark(2, y1); // X
        Act_Mark(2, y0); // O

        Assert_EventsObserved(
            new XMarkedEvent(0, y2),
            new OMarkedEvent(0, y0),
            new XMarkedEvent(1, y1),
            new OMarkedEvent(1, y0),
            new XMarkedEvent(2, y1),
            new OMarkedEvent(2, y0),
            new OWinsEvent()
        );
    }

    [Test]
    public void XBlockedAlongTop()
    {
        Act_Mark(0, 0); // X
        Act_Mark(0, 1); // O
        Act_Mark(1, 0); // X
        Act_Mark(2, 0); // O
        Act_Mark(2, 1); // X

        Assert_EventNotObserved(Arg.Any<XWinsEvent>());
    }

    [Test]
    public void XWinsVertical([Range(0, 2)] int x0)
    {
        var x1 = (x0 + 1) % 3;

        Act_Mark(x0, 0); // X
        Act_Mark(x1, 0); // O
        Act_Mark(x0, 1); // X
        Act_Mark(x1, 1); // O
        Act_Mark(x0, 2); // X

        Assert_EventsObserved(
            new XMarkedEvent(x0, 0),
            new OMarkedEvent(x1, 0),
            new XMarkedEvent(x0, 1),
            new OMarkedEvent(x1, 1),
            new XMarkedEvent(x0, 2),
            new XWinsEvent()
        );
    }

    [Test]
    public void XWinsDownDiagonal()
    {
        Act_Mark(0, 0); // X
        Act_Mark(1, 0); // O
        Act_Mark(1, 1); // X
        Act_Mark(0, 1); // O
        Act_Mark(2, 2); // X

        Assert_EventsObserved(
            new XMarkedEvent(0, 0),
            new OMarkedEvent(1, 0),
            new XMarkedEvent(1, 1),
            new OMarkedEvent(0, 1),
            new XMarkedEvent(2, 2),
            new XWinsEvent()
        );
    }

    [Test]
    public void XWinsUpDiagonal()
    {
        Act_Mark(0, 2); // X
        Act_Mark(1, 0); // O
        Act_Mark(1, 1); // X
        Act_Mark(0, 1); // O
        Act_Mark(2, 0); // X

        Assert_EventsObserved(
            new XMarkedEvent(0, 2),
            new OMarkedEvent(1, 0),
            new XMarkedEvent(1, 1),
            new OMarkedEvent(0, 1),
            new XMarkedEvent(2, 0),
            new XWinsEvent()
        );
    }
}