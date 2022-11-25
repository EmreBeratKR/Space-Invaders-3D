using BulletSystem;
using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class InvaderBullet : Bullet<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private Rigidbody body;
        
        
        private Invader m_Shooter;
        
        
        public override void Shoot(Invader shooter, Vector3 position, Vector3 direction, float speed)
        {
            m_Shooter = shooter;
            var bodyTransform = body.transform;
            bodyTransform.position = position;
            bodyTransform.up = direction;
            body.velocity = direction * speed;
        }
    }
}