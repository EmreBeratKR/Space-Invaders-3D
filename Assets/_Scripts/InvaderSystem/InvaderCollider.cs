using SpaceShipSystem;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderCollider : BehaviourModule<Invader>
    {
        public void HitBySpaceShipBullet(SpaceShip spaceShip)
        {
            var response = new Invader.EventResponse()
            {

            };
            
            MainBehaviour.OnHitBySpaceShipBullet?.Invoke(response);
        }
    }
}