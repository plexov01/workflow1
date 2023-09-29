namespace WorkFlow1.Features.Bot
{
	using UnityEngine;

	/// <summary>
	/// Абстрактный бот
	/// </summary>
	public abstract class AbstractBot : MonoBehaviour
	{
		public int Id = default;

		[SerializeField] protected BotController botController = default;


		protected virtual void Awake()
		{
			if (botController == null)
			{
				botController = GetComponent<BotController>();
			}

			botController.Initialize();
		}

		/// <summary>
		/// Применение урона к текущему бота
		/// </summary>
		public abstract void ApplyDamage(int damage);
	}
}