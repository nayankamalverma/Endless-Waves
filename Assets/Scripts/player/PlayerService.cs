using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.player
{
    public class PlayerService
    {
        private PlayerController controller;
        private EventService eventService;

        public PlayerService(EventService eventService, PlayerScriptableObject playerSO, PlayerView playerView)
        {
            this.eventService = eventService;
            CreatePlayer(playerSO,playerView);
        }

        private void CreatePlayer(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            controller = new PlayerController(playerSO, playerView);
        }
    }
}