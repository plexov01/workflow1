namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

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

		private void OnEnable()
		{
			botController.ReturnDefaultBotData();
			FindEnemy(null);
		}

		private void Start() => FindEnemy(null);

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
			if (enemyKiller == gameObject)
			{
				botController.IncreaseScore();
				botController.IncreaseDamage();
			}

			if (enemy == _enemyGameObject)
			{
				FindEnemy(null);
				enemy.GetComponent<IKillable>().OnDied -= DieEnemy;
			}
		}

		/// <summary>
		/// Найти врага
		/// </summary>
		/// <param name="enemy"></param>
		public void FindEnemy(GameObject enemy)
		{
			// Поиск среди активных или нахождение конкретного бота для атаки
			_enemyGameObject = enemy != null ? enemy : botController.StartChaseRandomBot();

			if (_enemyGameObject != null)
			{
				_enemyBot = _enemyGameObject.GetComponent<AbstractBot>();
				_enemyGameObject.GetComponent<IKillable>().OnDied += DieEnemy;
			}
			else
			{
				botController.WaitUntilEnemyAppear();
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