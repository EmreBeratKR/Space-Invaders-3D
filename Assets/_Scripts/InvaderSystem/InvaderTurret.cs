using BulletSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderTurret : BehaviourModule<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private BulletSpawner<Invader> bulletSpawner;
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


        private void OnShootCommand(Invader.EventResponse response)
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
                MainBehaviour.OnShootCommand += OnShootCommand;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnShootCommand -= OnShootCommand;
            }
        }
    }
}