namespace WorkFlow1.Features.Bot
{
	using System.Collections;
	using UnityEngine;

	public class AttackBehavior : AbstractBotBehavior
	{
		public string nameScipt = nameof(AttackBehavior);
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
				_enemy.ApplyDamage(_damage);
				yield return null;
			}
		}

		public override void EnterBehavior()
		{
			if (_bot.isActiveAndEnabled)
			{
				_bot.StartCoroutine(DamageEnemy());
			}
				
		}

		public override void ExitBehavior()
		{
		}
	}
}