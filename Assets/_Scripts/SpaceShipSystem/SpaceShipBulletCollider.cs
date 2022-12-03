using BulletSystem;

namespace SpaceShipSystem
{
    public class SpaceShipBulletCollider : BulletCollider<SpaceShip>
    {
        public static explicit operator SpaceShipBullet(SpaceShipBulletCollider invaderBulletCollider)
        {
            return (SpaceShipBullet) invaderBulletCollider.MainBehaviour;
        }
    }
}