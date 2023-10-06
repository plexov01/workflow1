namespace WorkFlow1.Features.Bot
{
	using System.Collections;
	using UnityEngine;

	/// <summary>
	/// Реализация поведения атаки бота
	/// </summary>
	public class AttackBehavior : AbstractBotBehavior
	{
		private AbstractBot _bot = default;
		private AbstractBot _enemy = default;
		private int _damage = default;
		private Coroutine _damageCoroutine = default;

		public AttackBehavior(AbstractBot bot, AbstractBot enemyBot, int damage)
		{
			_bot = bot;
			_enemy = enemyBot;
			_damage = damage;
		}

		private IEnumerator DamageEnemy()
		{
			while (_enemy.isActiveAndEnabled && _bot.isActiveAndEnabled)
			{
				_enemy.ApplyDamage(_bot, _damage);
				yield return null;
			}
		}

		public override void EnterBehavior()
		{
			if (_bot.isActiveAndEnabled)
			{
				_damageCoroutine = _bot.StartCoroutine(DamageEnemy());
			}
		}

		public override void ExitBehavior()
		{
			if (_damageCoroutine != null)
			{
				_bot.StopCoroutine(_damageCoroutine);
			}
		}
	}
}