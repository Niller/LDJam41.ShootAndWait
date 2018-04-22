namespace DefaultNamespace
{
    public class HardDifficulty : BaseDifficultySettings
    {
        private float _enemiesActionPoints = 16;
        private int _playerHp = 50;

        public override float EnemiesActionPoints
        {
            get { return _enemiesActionPoints; }
        }

        public override int PlayerHp
        {
            get { return _playerHp; }
        }
    }
}