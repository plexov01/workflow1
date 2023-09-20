namespace WorkFlow1.Features.Bot
{
	using System;

	/// <summary>
	/// Класс абстрактного поведения бота
	/// </summary>
	[Serializable]
	public abstract class AbstractBotBehavior
	{
		/// <summary>
		/// Начать следовать поведению
		/// </summary>
		public abstract void EnterFollowBehavior();

		/// <summary>
		/// Следовать поведению соответствующему текущему состоянию бота
		/// </summary>
		public abstract void FollowBehavior();

		/// <summary>
		/// Перестать следовать поведению
		/// </summary>
		public abstract void ExitFollowBehavior();
	}
}