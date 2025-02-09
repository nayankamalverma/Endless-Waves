namespace Assets.Scripts.player
{
    public class PlayerModel
    {
        private float moveSpeed;
        private int maxHealth = 100;
        private int currentHealth;

        public PlayerModel()
        {
            currentHealth = maxHealth;
        }

        public float GetMoveSpeed() => moveSpeed;
        public int GetMaxHealth() => maxHealth;
        public int GetCurrentHealth() => currentHealth;
    }
}