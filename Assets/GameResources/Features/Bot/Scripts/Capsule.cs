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
			if (_enemyGameObject != null)
			{
				_enemyBot = _enemyGameObject.GetComponent<AbstractBot>();

				_enemyGameObject.GetComponent<IKillable>().OnDied += DieEnemy;
			}
			
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

		private void DoDamage(AbstractBot bot) => botController.AttackBot(bot);

		private void DieEnemy(GameObject enemy)
		{
			if (enemy == _enemyGameObject)
			{
				// TODO: Осуществить поиск новой цели
				_enemyGameObject = botController.StartChaseRandomBot();
				if (_enemyGameObject != null)
				{
					_enemyBot = _enemyGameObject.GetComponent<AbstractBot>();

					_enemyGameObject.GetComponent<IKillable>().OnDied += DieEnemy;
				}

				Debug.Log(gameObject.name + " " + enemy.gameObject.name);
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