using BulletSystem;

namespace SpaceShipSystem
{
    public class SpaceShipBulletSpawner : BulletSpawner<SpaceShip>
    {
        public bool HasActiveBullet => Pool.CountActive > 0;
    }
}