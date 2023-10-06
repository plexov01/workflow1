namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;
	using UnityEngine.AI;

	/// <summary>
	/// Реализация поведения движения к определённой точке
	/// </summary>
	[Serializable]
	public class MoveBehavior : AbstractBotBehavior
	{
		[SerializeField] private GameObject _aim = default;

		private NavMeshAgent _navMeshAgent = default;

		public MoveBehavior(NavMeshAgent navMeshAgent, GameObject attackedBot)
		{
			_navMeshAgent = navMeshAgent;
			_aim = attackedBot;
		}

		public override void EnterBehavior()
		{
			if (_aim.TryGetComponent(out IMovable botMovable))
			{
				botMovable.OnPositionChanged += ChaseAim;
			}
		}

		public override void ExitBehavior()
		{
			if (_aim.TryGetComponent(out IMovable botMovable))
			{
				botMovable.OnPositionChanged -= ChaseAim;
			}
		}

		private void ChaseAim()
		{
			if (_navMeshAgent.gameObject.activeInHierarchy)
			{
				_navMeshAgent.SetDestination(_aim.transform.position);
			}
		}
	}
}