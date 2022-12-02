using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipAnimatorEventHandler : BehaviourModule<SpaceShip>
    {
        private void OnDieAnimationComplete()
        {
            MainBehaviour.OnDieAnimationComplete?.Invoke(new SpaceShip.EventResponse());
        }
    }
}