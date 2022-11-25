using InvaderSystem;
using UnityEngine;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipCollider : BehaviourModule<SpaceShip>
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
        
        
        public void OnShotByInvaderBullet(Invader shooter)
        {
            Collider.enabled = false;

            var response = new SpaceShip.EventResponse()
            {

            };
            
            MainBehaviour.OnShotByInvader?.Invoke(response);
        }
    }
}