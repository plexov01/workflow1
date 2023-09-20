namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Реализация поведения атаки бота
	/// </summary>
	[Serializable]
	public class AttackBehavior : AbstractBotBehavior
	{
		[SerializeField] private GameObject _aim = default;


		public override void EnterFollowBehavior()
		{
			Debug.Log("AttackBehavior StartFollowBehavior()");
			//TODO: Реализовать действия при входе в поведение
		}

		public override void FollowBehavior()
		{
			Debug.Log("AttackBehavior FollowBehavior()");
			//TODO: Реализовать действия поведения
		}

		public override void ExitFollowBehavior()
		{
			Debug.Log("AttackBehavior ExitFollowBehavior()");
			//TODO: Реализовать действия при выходе из поведение
		}
	}
}