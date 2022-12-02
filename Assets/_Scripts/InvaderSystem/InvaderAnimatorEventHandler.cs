using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderAnimatorEventHandler : BehaviourModule<Invader>
    {
        private void OnDieAnimationComplete()
        {
            Game.Resume();
            
            var response = new Invader.EventResponse()
            {

            };
            
            MainBehaviour.OnDieAnimationComplete?.Invoke(response);
        }

        private void OnInvasionStepAnimation()
        {
            MainBehaviour.HandleInvasionStep();
        }
    }
}