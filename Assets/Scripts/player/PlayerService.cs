using Assets.Scripts.Utilities.ScriptableObjects;

namespace Assets.Scripts.player
{
    public class PlayerService
    {
        private PlayerController controller;

        public PlayerService(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            CreatePlayer(playerSO, playerView);
        }

        private void CreatePlayer(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            controller = new PlayerController(playerSO, playerView);
        }
    }
}