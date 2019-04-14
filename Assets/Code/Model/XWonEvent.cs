using System;

namespace Assets.Code.Model
{
	public class XWonEvent : GameEvent, IEquatable<XWonEvent>
	{
		public bool Equals(XWonEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as XWonEvent);

		public override int GetHashCode()
			=> nameof(XWonEvent).GetHashCode();

		public static bool operator ==(XWonEvent left, XWonEvent right)
			=> Equals(left, right);

		public static bool operator !=(XWonEvent left, XWonEvent right)
			=> !Equals(left, right);
	}
}