namespace WorkFlow1.Features.Bot
{
	using UnityEngine;

	/// <summary>
	/// Абстрактный бот
	/// </summary>
	public abstract class AbstractBot : MonoBehaviour
	{
		[SerializeField] protected string id = default;

		[SerializeField] protected BotData botData = new BotData();

		protected virtual void Awake()
		{
			botData.Initialize();
		}

		/// <summary>
		/// Нанесение урона другому боту
		/// </summary>
		/// <param name="bot"></param>
		/// <param name="damage"></param>
		public abstract void MakeDamage(AbstractBot bot, int damage);

		/// <summary>
		/// Нанесение урона себе
		/// </summary>
		public abstract void ApplyDamage();
	}
}