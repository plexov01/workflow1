namespace WorkFlow1.Features.Spawner
{
	using UnityEngine;
	using Camera;
	using Bot;

	/// <summary>
	/// Класс создающий бота при нажатии на него мышью
	/// </summary>
	public class SpawnOnPlaneByMouse : MonoBehaviour
	{
		private Camera _camera = default;
		private BotCreator _botCreator = default;

		private void Awake()
		{
			_botCreator = FindObjectOfType<BotCreator>();
		}

		private void Start()
		{
			_camera = FindObjectOfType<MainCamera>().Camera;
		}

		private void OnMouseDown()
		{
			RaycastHit hit;
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				_botCreator.CreateBot(hit.point+Vector3.up, Quaternion.identity);
			}
		}
	}
}