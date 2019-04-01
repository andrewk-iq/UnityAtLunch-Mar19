using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameTests : GameTestFixture
{
	[Test]
	public void XMarkedAt00()
	{
		Act_MarkX(0, 0);

		Assert_EventObserved(new XMarkedEvent(0, 0));
	}

	[Test]
	public void XMarkedAt11()
	{
		Act_MarkX(1, 1);

		Assert_EventObserved(new XMarkedEvent(1, 1));
	}

	[Test]
	public void XMarkedAt01()
	{
		Act_MarkX(0, 1);

		Assert_EventObserved(new XMarkedEvent(0, 1));
	}
}