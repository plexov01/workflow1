namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;
	using UnityEngine.AI;
	using System.Collections.Generic;
	using BotPool;
	using UIControllers;

	/// <summary>
	/// Контроллер бота
	/// </summary>
	[Serializable]
	public class BotController : MonoBehaviour
	{
		[SerializeField] private BotData _botData = new BotData();

		private GameObject _bot = default;

		private NavMeshAgent _navMeshAgent = default;

		private BotStateMachine _botStateMachine = default;

		private BotPool _botPool = default;

		private UIBotViewController _uiBotViewController = default;

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

			if (_uiBotViewController == null)
			{
				_uiBotViewController = _bot.transform.GetComponentInChildren<UIBotViewController>();
			}

			int newId = _botPool.GetNewBotId();
			_bot.name += newId;

			_botData.Initialize(newId);

			_navMeshAgent.speed = _botData.Speed;

			_uiBotViewController.CurrentHealthPercentage = _botData.CurrentHealth / _botData.MaxHealth;
			_uiBotViewController.CurrentScore = _botData.Score;
			_uiBotViewController.UpdateUI();
		}

		/// <summary>
		/// Начать атаку на случайного бота
		/// </summary>
		/// <returns></returns>
		public GameObject StartChaseRandomBot()
		{
			var listEnemies = new List<GameObject>(_botPool.GetListActiveBots());

			//Удаление текущего бота из списка возможных целей
			listEnemies.Remove(_bot);

			if (listEnemies.Count == 0)
			{
				_botStateMachine.ChangeState(new IdleBehavior());
				return null;
			}

			//Получение рандомного бота из списка активных
			GameObject attackedBot = listEnemies[UnityEngine.Random.Range(0, listEnemies.Count)].gameObject;

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
		public int GetHealth() => _botData.CurrentHealth;

		/// <summary>
		/// Уменьшить здоровье бота
		/// </summary>
		/// <param name="health"></param>
		public void DecreaseHealth(int health)
		{
			_botData.CurrentHealth -= health;
			_uiBotViewController.CurrentHealthPercentage = (float)_botData.CurrentHealth / (float)_botData.MaxHealth;
			_uiBotViewController.UpdateUI();
		}

		/// <summary>
		/// Увеличить счёт убитых фрагов
		/// </summary>
		public void IncreaseScore()
		{
			_botData.Score++;
			_uiBotViewController.CurrentScore = _botData.Score;
			_uiBotViewController.UpdateUI();
		}

		/// <summary>
		/// Увеличить урон бота
		/// </summary>
		public void IncreaseDamage() => _botData.Damage++;

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