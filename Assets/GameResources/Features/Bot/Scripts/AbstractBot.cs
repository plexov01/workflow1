namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	using BotPool;

	/// <summary>
	/// Абстрактный бот
	/// </summary>
	public abstract class AbstractBot : MonoBehaviour
	{
		protected BotController botController = default;
		protected BotPool botPool = default;

		protected virtual void Awake()
		{
			botController = GetComponent<BotController>();
			botPool = FindObjectOfType<BotPool>();


			if (botController == null)
			{
				Debug.Log("Отсутствует BotController на " + gameObject.name);
			}

			if (botPool == null)
			{
				Debug.Log("BotPool не найден");
			}

			int newId = botPool.GetNewBotId();

			botController.Initialize(newId);
		}

		/// <summary>
		/// Применение урона к текущему боту
		/// </summary>
		public abstract void ApplyDamage(AbstractBot enemy, int damage);
	}
}