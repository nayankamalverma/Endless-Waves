using Assets.Scripts.UI.ScriptableObjects;

namespace Assets.Scripts.player
{
    public class PlayerModel
    {
        private PlayerScriptableObject playerSO;
        private int currentHealth;

        public PlayerModel(PlayerScriptableObject playerSO)
        {
            this.playerSO = playerSO;
            currentHealth = playerSO.maxHealth;
        }

        public float GetMoveSpeed() => playerSO.moveSpeed;
        public int GetMaxHealth() => playerSO.maxHealth;
        public int GetCurrentHealth() => currentHealth;
        public void ReduceHealth(int health) => currentHealth -= health;
        public void Heal(int health) => currentHealth += health;
        public void SetCurrentHealthToMax() => currentHealth = playerSO.maxHealth;
    }
}