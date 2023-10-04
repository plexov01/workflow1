namespace WorkFlow1.Features.Bot
{
	using System.Collections.Generic;
	using UnityEngine;
	using Spawner;
	using BotPool;

	/// <summary>
	/// Срздатель ботов на сцене
	/// </summary>
	public class BotCreator : MonoBehaviour
	{
		[SerializeField, Range(0, 12)] private int _botsCount = default;
		private GameObject _capsuleBotPrefab = default;

		private List<Transform> _spawnsTransform = new List<Transform>();

		private Spawner _spawner = default;
		private BotPool _botPool = default;
		private SpawnsPool _spawnsPool = default;

		private void Awake()
		{
			_capsuleBotPrefab = Resources.Load<GameObject>("Bot/Prefabs/Capsule");

			_spawner = FindObjectOfType<Spawner>();
			_botPool = FindObjectOfType<BotPool>();
			_spawnsPool = FindObjectOfType<SpawnsPool>();
			_spawnsTransform = _spawnsPool.GetSpawnTransforms();
		}

		private void Start()
		{
			for (int i = 0; i < _botsCount; i++)
			{
				Vector3 botPosition;
				Quaternion botRotation;

				// GameObject instanceGameObject;
				if (i < _spawnsTransform.Count)
				{
					botPosition = _spawnsTransform[i].position;
					botRotation = _spawnsTransform[i].rotation;
				}
				else
				{
					botPosition = Vector3.zero;
					botRotation = Quaternion.identity;
				}

				CreateBot(botPosition, botRotation);
			}
		}

		/// <summary>
		/// Создание бота в определённых координатах
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		public void CreateBot(Vector3 position, Quaternion rotation)
		{
			GameObject bot = _botPool.GetBot();
			if (bot == null)
			{
				bot = _spawner.CreateEntity(_capsuleBotPrefab, position, rotation,
					_botPool.transform);
				_botPool.AddBot(bot);
			}
			else
			{
				bot.transform.position = position;
				bot.transform.rotation = rotation;
			}
		}
	}
}