using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameFacade
    {
        private readonly SignalBus _signalBus;

        public GameFacade(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void StartGame()
        {
            _signalBus.Fire(new RestartGameSignal());
        }

        public void RestartGame()
        {
            _signalBus.Fire(new RestartGameSignal());
        }
    }
}
