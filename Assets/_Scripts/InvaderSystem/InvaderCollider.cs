using SpaceShipSystem;
using UnityEngine;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderCollider : BehaviourModule<Invader>
    {
        private Collider Collider
        {
            get
            {
                if (!m_Collider)
                {
                    m_Collider = GetComponent<Collider>();
                }

                return m_Collider;
            }
        }


        private Collider m_Collider;
        
        
        public void HitBySpaceShipBullet(SpaceShip spaceShip)
        {
            Collider.enabled = false;

            var response = new Invader.EventResponse()
            {

            };
            
            MainBehaviour.OnHitBySpaceShipBullet?.Invoke(response);
        }
    }
}