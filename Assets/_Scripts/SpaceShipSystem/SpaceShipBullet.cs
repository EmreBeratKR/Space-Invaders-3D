using BulletSystem;
using InvaderSystem;
using UnityEngine;
using Utils;

namespace SpaceShipSystem
{
    public class SpaceShipBullet : Bullet<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private Rigidbody body;


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


        protected override void OnHitByBullet()
        {
            base.OnHitByBullet();
        }


        private void CheckEnemyHit(Collider other)
        {
            if (!other.TryGetComponent(out InvaderCollider invaderCollider)) return;
            
            invaderCollider.OnShotBySpaceShipBullet(m_Shooter);
            Release();
        }
    }
}