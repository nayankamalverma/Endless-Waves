using Assets.Scripts.UI.ScriptableObjects;
using Assets.Scripts.Utilities.Events;

namespace Assets.Scripts.player
{
    public class PlayerService
    {
        private PlayerController controller;
        private EventService eventService;

        public PlayerService(EventService eventService, PlayerScriptableObject playerSO, PlayerView playerView)
        {
            this.eventService = eventService;
            CreatePlayer(playerSO, playerView);
        }

        private void CreatePlayer(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            controller = new PlayerController(eventService, playerSO, playerView);
        }
    }
}