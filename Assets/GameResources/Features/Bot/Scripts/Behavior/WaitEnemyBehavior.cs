namespace WorkFlow1.Features.Bot
{
    using UnityEngine;
    using BotPool;
    
    public class WaitEnemyBehavior : AbstractBotBehavior
    {
        public string nameScipt = nameof(WaitEnemyBehavior);
        
        private AbstractBot _bot = default;
        private BotPool _botPool = default;

        public WaitEnemyBehavior(AbstractBot bot)
        {
            _bot = bot;
            _botPool = Object.FindObjectOfType<BotPool>();
        }
        
        public override void EnterBehavior() => _botPool.OnEnemyAppeared += DoAftherAppearEnemy;

        public override void ExitBehavior() => _botPool.OnEnemyAppeared -= DoAftherAppearEnemy;
        /// <summary>
        /// Сделать после появления врага
        /// </summary>
        /// <param name="bot"></param>
        public void DoAftherAppearEnemy(GameObject bot)
        {
            _bot.GetComponent<ISearchable>().FindEnemy(bot);
        }
    }

}
