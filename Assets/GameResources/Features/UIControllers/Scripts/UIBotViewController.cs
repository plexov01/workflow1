namespace WorkFlow1.Features.UIControllers
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Класс управления UI бота
	/// </summary>
	public class UIBotViewController : MonoBehaviour
	{
		public int CurrentScore = default;
		[Range(0, 1)] public float CurrentHealthPercentage = default;

		[SerializeField] private Text _scoreText = default;
		[SerializeField] private Image _healthBarImage = default;

		private void Awake()
		{
			if (_healthBarImage == null)
			{
				Debug.Log("Не установлена ссылка на healthBarImage");
			}

			if (_scoreText == null)
			{
				Debug.Log("Не установлена ссылка на scoreText");
			}
		}

		/// <summary>
		/// Обновить UI бота
		/// </summary>
		public void UpdateUI()
		{
			_healthBarImage.fillAmount = CurrentHealthPercentage;
			_scoreText.text = CurrentScore.ToString();
		}
	}
}