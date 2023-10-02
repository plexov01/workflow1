namespace WorkFlow1.Features.Bot
{
	using UnityEngine;
	/// <summary>
	/// Доступен к поиску врага
	/// </summary>
	public interface ISearchable
	{
		public void FindEnemy(GameObject enemy);
	}
}