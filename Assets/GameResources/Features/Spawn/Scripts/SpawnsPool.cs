namespace WorkFlow1.Features.Spawner
{
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	/// <summary>
	/// Пул точек спавна
	/// </summary>
	public class SpawnsPool : MonoBehaviour
	{
		[SerializeField] private List<Transform> _spawnTransforms = default;

		private void Awake()
		{
			_spawnTransforms = GetComponentsInChildren<Transform>().ToList();
			_spawnTransforms.Remove(transform);
		}
		/// <summary>
		/// Получить список точек спавна
		/// </summary>
		/// <returns></returns>
		public List<Transform> GetSpawnTransforms() => _spawnTransforms;
	}
}