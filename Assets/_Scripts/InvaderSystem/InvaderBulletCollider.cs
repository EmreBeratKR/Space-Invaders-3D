using BulletSystem;
using UnityEngine;

namespace InvaderSystem
{
    public class InvaderBulletCollider : BulletCollider<Invader>
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


        public void Enable()
        {
            Collider.enabled = true;
        }

        public void Disable()
        {
            Collider.enabled = false;
        }
        

        public static explicit operator InvaderBullet(InvaderBulletCollider invaderBulletCollider)
        {
            return (InvaderBullet) invaderBulletCollider.MainBehaviour;
        }
    }
}