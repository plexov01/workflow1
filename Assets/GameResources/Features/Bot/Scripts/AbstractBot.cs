namespace WorkFlow1.Features.Bot
{
	using UnityEngine;

	/// <summary>
	/// Абстрактный бот
	/// </summary>
	public abstract class AbstractBot : MonoBehaviour
	{
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
		/// Применение урона к текущему боту
		/// </summary>
		public abstract void ApplyDamage(AbstractBot enemy, int damage);
	}
}