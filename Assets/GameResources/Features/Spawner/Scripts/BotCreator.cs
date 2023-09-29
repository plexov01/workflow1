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
		[SerializeField, Range(0,12)] private int _botsCount = default;
		[SerializeField] private GameObject _capsuleBotPrefab = default;
		[SerializeField] private GameObject _spawnPointsPool = default;

		private List<Transform> _spawnsTransform = new List<Transform>();

		private Spawner _spawner = default;
		private BotPool _botPool = default;
		private SpawnsPool _spawnsPool = default;

		private void Awake()
		{
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
				Quaternion botQuaternion;

				GameObject instanceGameObject;
				if (i < _spawnsTransform.Count)
				{
					botPosition = _spawnsTransform[i].position;
					botQuaternion = _spawnsTransform[i].rotation;
				}
				else
				{
					botPosition = Vector3.zero;
					botQuaternion = Quaternion.identity;
				}

				instanceGameObject = _spawner.CreateEntity(_capsuleBotPrefab, botPosition, botQuaternion, _botPool.transform);
				
				_botPool.AddBot(instanceGameObject);
			}
		}
	}
}