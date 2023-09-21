namespace WorkFlow1.Features.Bot
{
	using System.Collections;
	using UnityEngine;

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
			while (_enemy.gameObject.activeInHierarchy && _bot.gameObject.activeInHierarchy)
			{
				_enemy.ApplyDamage(_damage);
				yield return new WaitForSecondsRealtime(0.1f);
			}
		}

		public override void EnterBehavior()
		{
			_damageCoroutine = _bot.StartCoroutine(DamageEnemy());
		}

		public override void ExitBehavior()
		{
			_bot.StopCoroutine(_damageCoroutine);
		}
	}
}