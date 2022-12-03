using BulletSystem;

namespace InvaderSystem
{
    public class InvaderBulletCollider : BulletCollider<Invader>
    {
        public static explicit operator InvaderBullet(InvaderBulletCollider invaderBulletCollider)
        {
            return (InvaderBullet) invaderBulletCollider.MainBehaviour;
        }
    }
}