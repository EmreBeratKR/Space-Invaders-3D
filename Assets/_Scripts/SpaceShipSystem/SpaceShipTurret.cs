using ScoreSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipTurret : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private SpaceShipBulletSpawner bulletSpawner;
        [SerializeField] private Transform muzzleTransform;

        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float shootSpeed;


        private Vector3 MuzzlePosition => muzzleTransform.position;
        private Vector3 ShootDirection => muzzleTransform.up;
        private bool CanShoot => !bulletSpawner.HasActiveBullet && !Game.IsPaused;
        

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnTurretShootInput(SpaceShip.EventResponse response)
        {
            Shoot();
        }
        
        
        private void Shoot()
        {
            if (!CanShoot) return;
            
            var newBullet = bulletSpawner.Spawn(MuzzlePosition);
            newBullet.Shoot(MainBehaviour, MuzzlePosition, ShootDirection, shootSpeed);
            
            MainBehaviour.OnTurretShootPerform?.Invoke(new SpaceShip.EventResponse());
            
            ScoreManager.IncrementUfoScoreTable();
        }
        
        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShootInput += OnTurretShootInput;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShootInput -= OnTurretShootInput;
            }
        }
    }
}