using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameTests : GameTestFixture
{
	[Test]
	public void XMarked()
	{
		Act_MarkX(0, 0);

		Assert_EventObserved(new XMarkedEvent(0, 0));
	}
}