namespace Assets.Code.Model
{
	public class RestartedEvent : GameEvent
	{
		protected bool Equals(RestartedEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as RestartedEvent);

		public override int GetHashCode()
			=> nameof(RestartedEvent).GetHashCode();

		public static bool operator ==(RestartedEvent left, RestartedEvent right)
			=> Equals(left, right);

		public static bool operator !=(RestartedEvent left, RestartedEvent right)
			=> !Equals(left, right);
	}
}