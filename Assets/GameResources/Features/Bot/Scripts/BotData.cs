namespace WorkFlow1.Features.Bot
{
	using System;
	using UnityEngine;

	/// <summary>
	/// Данные бота
	/// </summary>
	[Serializable]
	public class BotData
	{
		/// <summary>
		/// Здоровье бота
		/// </summary>
		[Min(0)] public int Health = default;

		[SerializeField] private int _id = default;
		[SerializeField, Min(0)] private float _speed = default;
		[SerializeField, Min(0)] private int _damage = default;

		/// <summary>
		/// Уникальный идентификатор бота
		/// </summary>
		public int Id => _id;

		/// <summary>
		/// Скорость передвижения бота
		/// </summary>
		public float Speed => _speed;

		/// <summary>
		/// Урон бота
		/// </summary>
		public int Damage => _damage;

		/// <summary>
		/// Инициализация данных бота
		/// </summary>
		public void Initialize(int id)
		{
			_id = id;
			
			if (Health == 0)
			{
				Health = UnityEngine.Random.Range(3, 7);
			}

			if (_damage == 0)
			{
				_damage = UnityEngine.Random.Range(1, 2);
			}

			if (_speed == 0)
			{
				_speed = UnityEngine.Random.Range(3, 7);
			}
		}
		/// <summary>
		/// Вернуть характеристики бота в изначальное состояние
		/// </summary>
		public void ResetToDefault()
		{
			Health = UnityEngine.Random.Range(3, 7);
		}
	}
}