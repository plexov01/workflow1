namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;

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
			FindEnemy();
		}

		private void Update()
		{
			if (transform.hasChanged)
			{
				OnPositionChanged();
			}
		}

		public override void ApplyDamage(AbstractBot enemy, int damage)
		{
			botController.DecreaseHealth(damage);
			int currentHealth = botController.GetHealth();

			if (currentHealth <= 0)
			{
				_enemyGameObject.GetComponent<IKillable>().OnDied -= DieEnemy;
				Die();
				botController.GetIdleState();
				OnDied(enemy.gameObject, gameObject);
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
			StartChaseRandomBot();

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

		private void Die() => botPool.ReturnToPool(gameObject);

		private void StartChaseRandomBot()
		{
			var listEnemies = new List<GameObject>(botPool.GetListActiveBots());

			//Удаление текущего бота из списка возможных целей
			listEnemies.Remove(gameObject);

			if (listEnemies.Count == 0)
			{
				botController.WaitUntilEnemyAppear();
				return;
			}

			//Получение рандомного бота из списка активных
			_enemyGameObject = listEnemies[UnityEngine.Random.Range(0, listEnemies.Count)].gameObject;

			botController.Chase(_enemyGameObject);
		}
	}
}