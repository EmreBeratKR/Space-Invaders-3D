using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class BonusInvaderAnimatorEventHandler : BehaviourModule<BonusInvader>
    {
        private void OnDieAnimationComplete()
        {
            MainBehaviour.OnDieAnimationComplete?.Invoke(new BonusInvader.EventResponse());
        }
    }
}