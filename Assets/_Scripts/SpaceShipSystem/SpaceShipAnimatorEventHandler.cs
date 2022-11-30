using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipAnimatorEventHandler : BehaviourModule<SpaceShip>
    {
        private void OnDieAnimationComplete()
        {
            MainBehaviour.OnRespawn?.Invoke(new SpaceShip.EventResponse());
        }
    }
}