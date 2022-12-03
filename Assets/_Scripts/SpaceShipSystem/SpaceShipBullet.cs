using BulletSystem;
using InvaderSystem;
using UnityEngine;

namespace SpaceShipSystem
{
    public class SpaceShipBullet : Bullet<SpaceShip>
    {
        private SpaceShip m_Shooter;


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            CheckEnemyHit(other);
        }
        

        public override void Shoot(SpaceShip shooter, Vector3 position, Vector3 direction, float speed)
        {
            m_Shooter = shooter;
            var bodyTransform = body.transform;
            bodyTransform.position = position;
            bodyTransform.up = direction;
            body.velocity = direction * speed;
        }


        private void CheckEnemyHit(Collider other)
        {
            if (!other.TryGetComponent(out InvaderCollider invaderCollider)) return;
            
            invaderCollider.OnShotBySpaceShipBullet(m_Shooter);
            Release();
        }
    }
}