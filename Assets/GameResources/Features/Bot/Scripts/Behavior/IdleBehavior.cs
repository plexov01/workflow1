namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Поведение бездействия бота
	/// </summary>
	[Serializable]
	public class IdleBehavior : AbstractBotBehavior
	{
		public string nameScipt = nameof(IdleBehavior);
		public override void EnterBehavior()
		{
			//TODO: Реализовать действия при входе в поведение
		}

		public override void ExitBehavior()
		{
			//TODO: Реализовать действия при выходе из поведение
		}
	}
}