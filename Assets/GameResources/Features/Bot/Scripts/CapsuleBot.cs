namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;
	using BotPool;

	/// <summary>
	/// Бот цилиндр
	/// </summary>
	public class CapsuleBot : AbstractBot, IMovable, IKillable, ISearchable
	{
		/// <summary>
		/// Позиция изменилась
		/// </summary>
		public event Action OnPositionChanged = delegate { };

		/// <summary>
		/// Бот умер
		/// </summary>
		public event Action<GameObject, GameObject> OnDied = delegate { };

		private GameObject _enemyGameObject = default;
		private AbstractBot _enemyBot = default;
		private BotPool _botPool = default;

		protected override void Awake()
		{
			base.Awake();
			_botPool = FindObjectOfType<BotPool>();
		}

		private void OnEnable()
		{
			botController.ReturnDefaultBotData();
			FindEnemy();
		}

		private void Update()
		{
			if (transform.hasChanged)
			{
				OnPositionChanged();
			}
		}

		public override void ApplyDamage(GameObject enemy, int damage)
		{
			botController.DecreaseHealth(damage);
			int currentHealth = botController.GetHealth();

			if (currentHealth <= 0)
			{
				_enemyGameObject.GetComponent<IKillable>().OnDied -= DieEnemy;
				botController.Die();
				OnDied(enemy, gameObject);
			}
		}

		// Враг умер
		private void DieEnemy(GameObject enemyKiller, GameObject enemy)
		{
			enemy.GetComponent<IKillable>().OnDied -= DieEnemy;

			if (enemy == _enemyGameObject)
			{
				FindEnemy();
			}

			if (enemyKiller == gameObject)
			{
				botController.IncreaseScore();
				botController.IncreaseDamage();
			}
		}

		/// <summary>
		/// Найти врага
		/// </summary>
		/// <param name="enemy"></param>
		public void FindEnemy()
		{
			// Поиск бота для атаки
			_enemyGameObject = botController.StartChaseRandomBot();

			if (_enemyGameObject != null)
			{
				_enemyBot = _enemyGameObject.GetComponent<AbstractBot>();
				_enemyGameObject.GetComponent<IKillable>().OnDied += DieEnemy;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject == _enemyGameObject)
			{
				_enemyBot = other.gameObject.GetComponent<AbstractBot>();
				botController.AttackBot(_enemyBot);
			}
		}
	}
}