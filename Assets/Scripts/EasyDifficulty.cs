namespace DefaultNamespace
{
    public class EasyDifficulty : BaseDifficultySettings
    {
        private float _enemiesActionPoints = 10;
        private int _playerHp = 100;

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