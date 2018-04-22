namespace DefaultNamespace
{
    public class MediumDifficulty : BaseDifficultySettings
    {
        private float _enemiesActionPoints = 13;
        private int _playerHp = 80;

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