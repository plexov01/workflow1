namespace WorkFlow1.Features.Bot
{
	using System;
	using UnityEngine;

	public interface IKillable
	{
		public event Action<GameObject> OnDied;
	}
}