namespace WorkFlow1.Features.Spawner
{
	using UnityEngine;

	/// <summary>
	/// Спавнер объектов
	/// </summary>
	public class Spawner : MonoBehaviour
	{
		public GameObject CreateEntity(GameObject prefab, Vector3 position, Quaternion rotation,
			Transform parentTransform)
		{
			GameObject newInstace = Instantiate(prefab, position, rotation, parentTransform);
			return newInstace;
		}
	}
}