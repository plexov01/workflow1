namespace WorkFlow1.Features.BotPool
{
	using System.Collections.Generic;
	using UnityEngine;
	using System.Linq;
	using Bot;

	/// <summary>
	/// Пул объектов с ботами
	/// </summary>
	public class BotPool : MonoBehaviour
	{
		[SerializeField] private List<GameObject> _bots = new List<GameObject>();

		[SerializeField] private List<GameObject> _activeBots = new List<GameObject>();

		[SerializeField] private List<GameObject> _notActiveBots = new List<GameObject>();

		private int _counterId = default;

		/// <summary>
		/// Получить список активных ботов
		/// </summary>
		public List<GameObject> GetListActiveBots()
		{
			List<GameObject> activeBots = new List<GameObject>(from x in _bots where x.activeInHierarchy select x);

			return activeBots;
		}

		/// <summary>
		/// Добавление бота в пул с ботами
		/// </summary>
		/// <param name="bot"></param>
		public void AddBot(GameObject bot)
		{
			if (!_bots.Contains(bot))
			{
				_bots.Add(bot);
			}
		}

		/// <summary>
		/// Вернуть бота в пул
		/// </summary>
		/// <param name="bot"></param>
		public void ReturnToPool(GameObject bot)
		{
			bot.SetActive(false);
		}

		/// <summary>
		/// Получить новый id для бота
		/// </summary>
		/// <returns></returns>
		public int GetNewBotId()
		{
			return _counterId++;
		}
	}
}