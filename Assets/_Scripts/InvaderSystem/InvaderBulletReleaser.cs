using BulletSystem;

namespace InvaderSystem
{
    public class InvaderBulletReleaser : BulletReleaser<Invader>
    {
        protected override void OnReleased(BulletCollider<Invader> obj)
        {
            var bullet = (InvaderBullet) (InvaderBulletCollider) obj;
            bullet.OnBlast();
        }
    }
}