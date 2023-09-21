namespace WorkFlow1.Features.Bot
{
	using System;

	public interface IDamageable
	{
		public event Action OnDamaged;
	}
}