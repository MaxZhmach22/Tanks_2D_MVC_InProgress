using Tools;

namespace Tanks
{
    internal sealed class PlayerProfile
    {
        public readonly TankModel CurrentTank;
        public SubscriptionProperty<GameState> CurrentGameState;
        public SubscriptionProperty<DifficultType> CurrentDifficultType;

        public PlayerProfile(PlayerProfileConfig playerProfileConfig)
        {
            CurrentTank = CreateTankModel(playerProfileConfig);
            CurrentGameState = new SubscriptionProperty<GameState>(playerProfileConfig.GameState);
            CurrentDifficultType = new SubscriptionProperty<DifficultType>(playerProfileConfig.DifficultType);
        }

        private TankModel CreateTankModel(PlayerProfileConfig playerProfileConfig)
        {
            return new TankModel(playerProfileConfig.TankSettings.Heatlh,
                                playerProfileConfig.TankSettings.Defense,
                                playerProfileConfig.TankType);
        }
    }
}
