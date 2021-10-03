namespace Tanks
{
    internal sealed class TankModel
    {
        private float _health;
        private float _defense;
        private TankType _tankType;
        public float Defense { get => _defense;}
        public float Health { get => _health; }
        internal TankType TankType { get => _tankType; }

        public TankModel(float health, float defense, TankType tankType)
        {
            _health = health;
            _defense = defense;
            _tankType = tankType;
        }



    }
}