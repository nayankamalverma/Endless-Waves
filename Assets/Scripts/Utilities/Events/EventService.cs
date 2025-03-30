using Assets.Scripts.Enemy;

namespace Assets.Scripts.Utilities.Events
{
    public class EventService
    {
        public EventController OnGameStart;
        public EventController OnGamePause;
        public EventController OnGameResume;
        public EventController<int> OnGameOver;
        public EventController OnGameOverPauseMenu;
        public EventController OnMainMenuButtonClicked;

        public EventController StartNextWave;
        public EventController<int, float> StartEnemySpawn;

        public EventController<int> OnPlayerHurt;
        public EventController<EnemyController> OnEnemyHurt;
        public EventController OnEnemyKilled;

        public EventService()
        {
            OnGameStart = new EventController();
            OnGamePause = new EventController();
            OnGameResume = new EventController();
            OnGameOver = new EventController<int>();
            OnGameOverPauseMenu = new EventController();
            OnMainMenuButtonClicked = new EventController();

            StartNextWave = new EventController();
            StartEnemySpawn = new EventController<int, float>();

            OnPlayerHurt = new EventController<int>();
            OnEnemyHurt = new EventController<EnemyController>();
            OnEnemyKilled = new EventController();
        }
    }
}