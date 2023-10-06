namespace WorkFlow1.Features.Spawner
{
	using UnityEngine;

	/// <summary>
	/// Спавнер объектов
	/// </summary>
	public class Spawner : MonoBehaviour
	{
		/// <summary>
		/// Создать объект
		/// </summary>
		/// <param name="prefab"></param>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <param name="parentTransform"></param>
		/// <returns></returns>
		public GameObject CreateEntity(GameObject prefab, Vector3 position, Quaternion rotation,
			Transform parentTransform)
		{
			GameObject newInstace = Instantiate(prefab, position, rotation, parentTransform);
			return newInstace;
		}
	}
}