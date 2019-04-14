using System;

namespace Assets.Code.Model
{
	public class OWonEvent : GameEvent, IEquatable<OWonEvent>
	{
		public bool Equals(OWonEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as OWonEvent);

		public override int GetHashCode()
			=> nameof(OWonEvent).GetHashCode();

		public static bool operator ==(OWonEvent left, OWonEvent right)
			=> Equals(left, right);

		public static bool operator !=(OWonEvent left, OWonEvent right)
			=> !Equals(left, right);
	}
}