using System;
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
        [SerializeField, Min(0f)] private float fireRatePerSecond;
        
        
        private Vector3 MuzzlePosition => muzzleTransform.position;
        private Vector3 ShootDirection => muzzleTransform.up;
        private float ElapsedSecondsAfterLastFiring => Time.time - m_LastFiringTime;
        private float FiringInterval => 1f / fireRatePerSecond;


        private float m_LastFiringTime = float.NegativeInfinity;
        

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
            if (ElapsedSecondsAfterLastFiring < FiringInterval) return;
            
            var newBullet = bulletSpawner.Spawn(MuzzlePosition);
            newBullet.Shoot(MainBehaviour, MuzzlePosition, ShootDirection, shootSpeed);

            m_LastFiringTime = Time.time;
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