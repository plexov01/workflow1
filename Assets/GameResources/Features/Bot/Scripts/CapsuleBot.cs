namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Бот цилиндр
	/// </summary>
	public class CapsuleBot : AbstractBot, IMovable, IDamageable, IKillable, ISearchable
	{
		/// <summary>
		/// Позиция изменилась
		/// </summary>
		public event Action OnPositionChanged = delegate { };

		/// <summary>
		/// Боту нанесён урон
		/// </summary>
		public event Action OnDamaged = delegate { };

		/// <summary>
		/// Бот умер
		/// </summary>
		public event Action<GameObject> OnDied = delegate { };

		[SerializeField] private GameObject _enemyGameObject = default;
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


		public override void ApplyDamage(int damage)
		{
			botController.DecreaseHealth(damage);
			int currentHealth = botController.GetHealth();

			if (currentHealth > 0)
			{
				OnDamaged();
			}
			else
			{
				_enemyGameObject.GetComponent<IKillable>().OnDied -= DieEnemy;
				botController.Die();
				OnDied(gameObject);
			}
		}

		private void DoDamage(AbstractBot bot)
		{
			botController.AttackBot(bot);
		}

		// Враг умер
		private void DieEnemy(GameObject enemy)
		{
			if (enemy == _enemyGameObject)
			{
				
				// TODO: Осуществить поиск новой цели
				FindEnemy(null);

				Debug.Log(gameObject.name + " " + enemy.gameObject.name);
				enemy.GetComponent<IKillable>().OnDied -= DieEnemy;
			}
		}

		/// <summary>
		/// Найти врага
		/// </summary>
		/// <param name="enemy"></param>
		public void FindEnemy(GameObject enemy)
		{
			// Поиск среди активных или нахождение конкретного
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
				DoDamage(_enemyBot);
			}
		}
	}
}