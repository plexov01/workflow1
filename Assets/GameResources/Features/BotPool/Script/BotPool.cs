namespace WorkFlow1.Features.BotPool
{
	using System.Collections.Generic;
	using UnityEngine;
	using System.Linq;
	using System;

	/// <summary>
	/// Пул объектов с ботами
	/// </summary>
	public class BotPool : MonoBehaviour
	{
		/// <summary>
		/// Появился враг
		/// </summary>
		public event Action<GameObject> OnEnemyAppeared = delegate { };

		[SerializeField] private List<GameObject> _bots = new List<GameObject>();

		private int _counterId = default;

		/// <summary>
		/// Получить список активных ботов
		/// </summary>
		public List<GameObject> GetListActiveBots()
		{
			var activeBots = new List<GameObject>(from x in _bots where x.activeInHierarchy select x);

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
				if (bot.activeInHierarchy)
				{
					OnEnemyAppeared(bot);
				}
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
		/// Получение неактивного бота
		/// </summary>
		/// <returns></returns>
		public GameObject GetNotActiveBot()
		{
			var bot = _bots.FirstOrDefault(x => !x.activeInHierarchy);

			return bot;
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