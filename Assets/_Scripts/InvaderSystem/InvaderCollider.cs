using System.Collections;
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
        
        
        public void OnShotBySpaceShipBullet(SpaceShip spaceShip)
        {
            Collider.enabled = false;

            MainBehaviour.OnShotBySpaceShip?.Invoke(new Invader.EventResponse());
        }

        public void OnReachInvasionBorder()
        {
            MainBehaviour.OnReachInvasionBorder?.Invoke(new Invader.EventResponse());
        }


        public static explicit operator Invader(InvaderCollider invaderCollider)
        {
            return invaderCollider.MainBehaviour;
        }
    }
}