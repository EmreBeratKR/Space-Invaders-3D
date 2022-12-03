using BulletSystem;
using SpaceShipSystem;
using UnityEngine;

namespace InvaderSystem
{
    public class InvaderBullet : Bullet<Invader>
    {
        private Invader m_Shooter;


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            CheckSpaceShipHit(other);
        }
        
        
        public override void Shoot(Invader shooter, Vector3 position, Vector3 direction, float speed)
        {
            m_Shooter = shooter;
            var bodyTransform = body.transform;
            bodyTransform.position = position;
            bodyTransform.up = direction;
            body.velocity = direction * speed;
        }


        private void CheckSpaceShipHit(Collider other)
        {
            if (!other.TryGetComponent(out SpaceShipCollider spaceShipCollider)) return;
            
            spaceShipCollider.OnShotByInvaderBullet(m_Shooter);
            Release();
        }
    }
}