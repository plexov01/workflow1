namespace WorkFlow1.Features.Bot
{
	using System;
	using UnityEngine;
	/// <summary>
	/// Доступен к убийству
	/// </summary>
	public interface IKillable
	{
		public event Action<GameObject,GameObject> OnDied;
	}
}