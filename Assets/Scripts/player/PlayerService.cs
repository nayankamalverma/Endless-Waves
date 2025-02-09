using Assets.Scripts.Utilities.Events;

namespace Assets.Scripts.player
{
    public class PlayerService
    {
        private PlayerController controller;
        private EventService eventService;

        public PlayerService(EventService eventService, PlayerView playerView)
        {
            this.eventService = eventService;
            CreatePlayer(playerView);
        }

        private void CreatePlayer(PlayerView playerView)
        {
            controller = new PlayerController(playerView);
        }
    }
}