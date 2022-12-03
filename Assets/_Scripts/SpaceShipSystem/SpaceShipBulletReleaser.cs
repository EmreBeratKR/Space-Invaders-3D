using BulletSystem;

namespace SpaceShipSystem
{
    public class SpaceShipBulletReleaser : BulletReleaser<SpaceShip>
    {
        protected override void OnReleased(BulletCollider<SpaceShip> obj)
        {
            var bullet = (SpaceShipBullet) (SpaceShipBulletCollider) obj;
            bullet.OnBlast();
        }
    }
}