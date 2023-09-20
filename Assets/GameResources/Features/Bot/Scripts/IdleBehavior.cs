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
		public override void EnterFollowBehavior()
		{
			Debug.Log("IdleBehavior EnterFollowBehavior");
			//TODO: Реализовать действия при входе в поведение
		}

		public override void FollowBehavior()
		{
			Debug.Log("IdleBehavior FollowBehavior");
			//TODO: Реализовать действия поведения
		}

		public override void ExitFollowBehavior()
		{
			Debug.Log("IdleBehavior ExitFollowBehavior");
			//TODO: Реализовать действия при выходе из поведение
		}
	}
}