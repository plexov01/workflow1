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
	public class BotController : MonoBehaviour
	{
		[SerializeField] private GameObject _bot = default;

		[SerializeField] private BotData _botData = new BotData();

		[SerializeField] private NavMeshAgent _navMeshAgent = default;

		[SerializeReference] private BotStateMachine _botStateMachine = default;

		[SerializeField] private BotPool _botPool = default;

		[SerializeField] private List<GameObject> _listEnemies = new List<GameObject>();

		/// <summary>
		/// Инициализация контроллера
		/// </summary>
		public void Initialize()
		{
			_bot = gameObject;

			if (_navMeshAgent == null)
			{
				_navMeshAgent = _bot.GetComponent<NavMeshAgent>();
			}

			if (_botStateMachine == null)
			{
				_botStateMachine = _bot.GetComponent<BotStateMachine>();
			}

			_botStateMachine.Initialize(new IdleBehavior());

			if (_botPool == null)
			{
				_botPool = FindObjectOfType<BotPool>();
			}

			int newId = _botPool.GetNewBotId();
			gameObject.name += newId;

			_botData.Initialize(newId);
			
			_navMeshAgent.speed = _botData.Speed;
		}

		/// <summary>
		/// Начать атаку на случайного бота
		/// </summary>
		/// <returns></returns>
		public GameObject StartChaseRandomBot()
		{
			_listEnemies = new List<GameObject>(_botPool.GetListActiveBots());

			//Удаление текущего бота из списка возможных целей
			_listEnemies.Remove(_bot);

			if (_listEnemies.Count == 0)
			{
				_botStateMachine.ChangeState(new IdleBehavior());
				return null;
			}

			GameObject attackedBot = _listEnemies[UnityEngine.Random.Range(0, _listEnemies.Count)].gameObject;

			_botStateMachine.ChangeState(new MoveBehavior(_navMeshAgent, attackedBot));

			return attackedBot;
		}

		/// <summary>
		/// Атаковать бота
		/// </summary>
		/// <param name="bot"></param>
		public void AttackBot(AbstractBot bot) =>
			_botStateMachine.ChangeState(new AttackBehavior(_bot.GetComponent<AbstractBot>(), bot, _botData.Damage));

		/// <summary>
		/// Получить текущее количество здоровья у бота
		/// </summary>
		/// <returns></returns>
		public int GetHealth() => _botData.Health;

		/// <summary>
		/// Уменьшить здоровье бота
		/// </summary>
		/// <param name="health"></param>
		public void DecreaseHealth(int health) => _botData.Health -= health;

		/// <summary>
		/// Бот умер
		/// </summary>
		public void Die()
		{
			_botStateMachine.ChangeState(new IdleBehavior());
			_botPool.ReturnToPool(_bot);
		}
		/// <summary>
		/// Ждать пока не появится враг
		/// </summary>
		public void WaitUntilEnemyAppear() =>
			_botStateMachine.ChangeState(new WaitEnemyBehavior(_bot.GetComponent<AbstractBot>()));

		/// <summary>
		/// Вернуть данные бота к дефолтному состоянию
		/// </summary>
		public void ReturnDefaultBotData() => _botData.ResetToDefault();
	}
}