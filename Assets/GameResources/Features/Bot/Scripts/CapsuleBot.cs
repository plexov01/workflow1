namespace WorkFlow1.Features.Bot
{
    using UnityEngine;
    /// <summary>
    /// Бот цилиндр
    /// </summary>
    public class CapsuleBot : AbstractBot
    {
        [SerializeField] private GameObject _enemyBot = default;
        
        public override void MakeDamage(AbstractBot bot, int damage)
        {
            //TODO: реализовать нанесение урона
        }

        public override void ApplyDamage()
        {
            //TODO: реализовать получение урона
        }
    }
}

