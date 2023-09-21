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
		public override void EnterBehavior()
		{
			Debug.Log("IdleBehavior EnterFollowBehavior");
			//TODO: Реализовать действия при входе в поведение
		}

		public override void ExitBehavior()
		{
			Debug.Log("IdleBehavior ExitFollowBehavior");
			//TODO: Реализовать действия при выходе из поведение
		}
	}
}