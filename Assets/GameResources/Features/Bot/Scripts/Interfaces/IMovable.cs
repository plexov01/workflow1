namespace WorkFlow1.Features.Bot
{
	using System;

	public interface IMovable
	{
		public event Action OnPositionChanged;
	}
}