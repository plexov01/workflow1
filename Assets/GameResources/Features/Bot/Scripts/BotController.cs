namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;
	using UnityEngine.AI;
	using System.Collections.Generic;
	using BotPool;

	/// <summary>
	/// Контроллер бота
	/// </summary>
	[Serializable]
	public class BotController
	{
		
		[SerializeField] private GameObject _bot = default;

		[SerializeField] private BotData _botData = new BotData();

		[SerializeField] private NavMeshAgent _navMeshAgent = default;

		[SerializeReference] private BotStateMachine _botStateMachine = new BotStateMachine();

		[SerializeField] private BotPool _botPool = default;

		[SerializeField] private List<AbstractBot> _listEnemies = default;

		/// <summary>
		/// Инициализация контроллера
		/// </summary>
		public void Initialize(GameObject gameObject)
		{
			_bot = gameObject;
			_botData.Initialize();
			_botStateMachine.Initialize(new IdleBehavior());

			if (_navMeshAgent == null)
			{
				_navMeshAgent = _bot.GetComponent<NavMeshAgent>();
			}

			if (_botPool==null)
			{
				_botPool = UnityEngine.Object.FindObjectOfType<BotPool>();
			}
			

			_navMeshAgent.speed = _botData.Speed;
		}

		/// <summary>
		/// Начать атаку на случайного бота
		/// </summary>
		/// <returns></returns>
		public GameObject StartAttackRandomBot()
		{
			_listEnemies = _botPool.GetListBots();

			//Удаление текущего бота из списка возможных целей
			_listEnemies.Remove(_bot.GetComponent<AbstractBot>());

			GameObject attackedBot = _listEnemies[UnityEngine.Random.Range(0, _listEnemies.Count)].gameObject;
			_navMeshAgent.SetDestination(attackedBot.transform.position);
			
			_botStateMachine.ChangeState(new AttackBehavior());

			return attackedBot;
		}
		/// <summary>
		/// Обновить позицию врага
		/// </summary>
		/// <param name="position"></param>
		public void UpdateEnemyPosition(Vector3 position)
		{
			_navMeshAgent.SetDestination(position);
		}
	}
}