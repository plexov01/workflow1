using UnityEngine.Serialization;

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
		/// Текущее здоровье бота
		/// </summary>
		[Min(0)] public int CurrentHealth = default;

		/// <summary>
		/// Счёт уничтоженных целей
		/// </summary>
		[Min(0)] public int Score = default;

		/// <summary>
		/// Урон бота
		/// </summary>
		[Min(0)] public int Damage = default;

		private int _id = default;
		[Min(0)] private float _speed = default;
		[Min(0)] private int _maxHealth = default;

		/// <summary>
		/// Уникальный идентификатор бота
		/// </summary>
		public int Id => _id;

		/// <summary>
		/// Скорость передвижения бота
		/// </summary>
		public float Speed => _speed;

		/// <summary>
		/// Максимальное здоровье бота
		/// </summary>
		public int MaxHealth => _maxHealth;

		/// <summary>
		/// Инициализация данных бота
		/// </summary>
		public void Initialize(int id)
		{
			_id = id;

			if (_maxHealth == 0)
			{
				_maxHealth = UnityEngine.Random.Range(3, 20);
				CurrentHealth = _maxHealth;
			}

			Score = 0;

			if (Damage == 0)
			{
				Damage = UnityEngine.Random.Range(1, 2);
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
			CurrentHealth = _maxHealth;
			Score = 0;
			Damage = UnityEngine.Random.Range(1, 2);;
		}
	}
}