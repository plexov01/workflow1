namespace WorkFlow1.Features.Bot
{
	using System;
	/// <summary>
	/// Доступен к нанесению урона
	/// </summary>
	public interface IDamageable
	{
		public event Action OnDamaged;
	}
}