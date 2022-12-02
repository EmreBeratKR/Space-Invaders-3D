using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class BonusInvaderAnimatorEventHandler : BehaviourModule<BonusInvader>
    {
        private void OnDieAnimationComplete()
        {
            Game.Resume();
            
            MainBehaviour.OnDieAnimationComplete?.Invoke(new BonusInvader.EventResponse());
        }
    }
}