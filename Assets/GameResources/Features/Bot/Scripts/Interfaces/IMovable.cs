namespace WorkFlow1.Features.Bot
{
	using System;
	/// <summary>
	/// Доступен к движению
	/// </summary>
	public interface IMovable
	{
		public event Action OnPositionChanged;
	}
}