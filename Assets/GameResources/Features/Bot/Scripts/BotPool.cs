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
		[SerializeField] private List<AbstractBot> _abstractBots = null;

		/// <summary>
		/// Получить список ботов
		/// </summary>
		public List<AbstractBot> GetListBots()
		{
			_abstractBots = new List<AbstractBot>();
			_abstractBots = FindObjectsOfType<AbstractBot>().ToList();
			// _abstractBots.Remove(gameObject.GetComponent<AbstractBot>());
			return _abstractBots;
		}
	}
}