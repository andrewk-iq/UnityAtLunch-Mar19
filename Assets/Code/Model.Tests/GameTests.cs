using NUnit.Framework;

public class GameTests : GameTestFixture
{
	[Test]
	public void XMarked([Range(0, 2)] int x, [Range(0, 2)] int y)
	{
		Act_Mark(x, y);

		Assert_EventObserved(new XMarkedEvent(x, y));
	}
}