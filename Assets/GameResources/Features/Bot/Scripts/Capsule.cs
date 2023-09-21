namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Бот цилиндр
	/// </summary>
	public class Capsule : AbstractBot, IMovable, IDamageable, IKillable
	{
		public event Action OnPositionChanged = delegate { };
		public event Action OnDamaged = delegate { };
		public event Action<GameObject> OnDied = delegate { };

		[SerializeField] private GameObject _enemyGameObject = default;
		private AbstractBot _enemyBot = default;

		private void Start()
		{
			_enemyGameObject = botController.StartChaseRandomBot();
			_enemyBot = _enemyGameObject.GetComponent<AbstractBot>();

			_enemyGameObject.GetComponent<IKillable>().OnDied += DieEnemy;
		}

		private void Update()
		{
			if (transform.hasChanged)
			{
				OnPositionChanged();
			}
			
			//TODO: Осуществлять постоянный поиск цели, если текущей цели нет
		}


		public override void ApplyDamage(int damage)
		{
			int currentHealth = botController.GetHealth();

			if (currentHealth - damage > 0)
			{
				botController.DecreaseHealth(damage);
				OnDamaged();
			}
			else
			{
				OnDied(gameObject);
				gameObject.SetActive(false);
			}
		}

		private void DoDamage(AbstractBot bot) => botController.AttackBot(bot);

		private void DieEnemy(GameObject enemy)
		{
			if (enemy == _enemyGameObject)
			{
				// TODO: Осуществить поиск новой цели
				enemy.GetComponent<IKillable>().OnDied -= DieEnemy;
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