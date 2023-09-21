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
		public abstract void EnterBehavior();

		/// <summary>
		/// Перестать следовать поведению
		/// </summary>
		public abstract void ExitBehavior();
	}
}