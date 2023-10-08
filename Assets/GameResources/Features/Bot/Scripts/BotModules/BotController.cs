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

		private UIBotViewController _uiBotViewController = default;

		/// <summary>
		/// Инициализация контроллера
		/// </summary>
		public void Initialize(int id)
		{
			_bot = gameObject;

			_navMeshAgent = _bot.GetComponent<NavMeshAgent>();

			_botStateMachine = _bot.GetComponent<BotStateMachine>();


			_uiBotViewController = _bot.transform.GetComponentInChildren<UIBotViewController>();


			if (_navMeshAgent == null)
			{
				_navMeshAgent = _bot.AddComponent<NavMeshAgent>();
			}

			if (_botStateMachine == null)
			{
				_botStateMachine = _bot.AddComponent<BotStateMachine>();
			}

			_botStateMachine.Initialize(new IdleBehavior());


			if (_uiBotViewController == null)
			{
				Debug.Log("На объекте нет UiBotViewController");
			}


			_bot.name += id;

			_botData.Initialize(id);

			_navMeshAgent.speed = _botData.Speed;
			UpdateCurrentBotUi();
		}

		private void UpdateCurrentBotUi()
		{
			_uiBotViewController.CurrentHealthPercentage = (float)_botData.CurrentHealth / _botData.MaxHealth;
			_uiBotViewController.CurrentScore = _botData.Score;
			_uiBotViewController.UpdateUI();
		}

		/// <summary>
		/// Начать преследовать цель
		/// </summary>
		/// <returns></returns>
		public void Chase(GameObject aim) => _botStateMachine.ChangeState(new MoveBehavior(_navMeshAgent, aim));

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
			UpdateCurrentBotUi();
		}

		/// <summary>
		/// Увеличить счёт убитых фрагов
		/// </summary>
		public void IncreaseScore()
		{
			_botData.Score++;
			UpdateCurrentBotUi();
		}

		/// <summary>
		/// Увеличить урон бота
		/// </summary>
		public void IncreaseDamage() => _botData.Damage++;

		/// <summary>
		/// Переход в состояние ожидания
		/// </summary>
		public void GetIdleState() => _botStateMachine.ChangeState(new IdleBehavior());

		/// <summary>
		/// Ждать пока не появится враг
		/// </summary>
		public void WaitUntilEnemyAppear() =>
			_botStateMachine.ChangeState(new WaitEnemyBehavior(_bot.GetComponent<AbstractBot>()));

		/// <summary>
		/// Вернуть данные бота к дефолтному состоянию
		/// </summary>
		public void ReturnDefaultBotData()
		{
			_botData.ResetToDefault();
			UpdateCurrentBotUi();
		}
	}
}