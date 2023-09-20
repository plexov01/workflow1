namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Машина состояний для бота
	/// </summary>
	[Serializable]
	public class BotStateMachine
	{
		[SerializeReference] private AbstractBotBehavior _currentBotBehavior = default;

		/// <summary>
		/// Инициализация начального поведения
		/// </summary>
		public void Initialize(AbstractBotBehavior _state)
		{
			_currentBotBehavior = _state;
			_state.EnterFollowBehavior();
		}

		/// <summary>
		/// Изменение текущего состояния
		/// </summary>
		/// <param name="_state"></param>
		public void ChangeState(AbstractBotBehavior _state)
		{
			_currentBotBehavior.ExitFollowBehavior();

			_currentBotBehavior = _state;

			_currentBotBehavior.EnterFollowBehavior();
		}
	}
}