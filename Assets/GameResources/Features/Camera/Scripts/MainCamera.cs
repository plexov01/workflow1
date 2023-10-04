namespace WorkFlow1.Features.Camera
{
	using UnityEngine;

	/// <summary>
	/// Класс, содержащий ссылку на камеру
	/// </summary>
	public class MainCamera : MonoBehaviour
	{
		/// <summary>
		/// Ссылка на основную камеру
		/// </summary>
		public Camera Camera = default;

		private void Awake()
		{
			if (Camera == null)
			{
				Camera = GetComponent<Camera>();
			}
		}
	}
}