using BulletSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipTurret : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private BulletSpawner<SpaceShip> bulletSpawner;
        [SerializeField] private Transform muzzleTransform;

        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float shootSpeed;
        
        private Vector3 MuzzlePosition => muzzleTransform.position;
        private Vector3 ShootDirection => muzzleTransform.up;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnTurretShoot(SpaceShip.EventResponse response)
        {
            Shoot();
        }
        
        
        private void Shoot()
        {
            var newBullet = bulletSpawner.Spawn(MuzzlePosition);
            newBullet.Shoot(MainBehaviour, MuzzlePosition, ShootDirection, shootSpeed);
        }
        
        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShoot += OnTurretShoot;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShoot -= OnTurretShoot;
            }
        }
    }
}