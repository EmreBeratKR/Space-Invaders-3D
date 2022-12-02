using UnityEngine;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipInput : BehaviourModule<SpaceShip>
    {
        private void Awake()
        {
            AddListeners();
            Disable();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
        
        private void Update()
        {
            HandleMovementInput();
            HandleTurretInput();
        }


        private void OnGameStart(Game.EventResponse response)
        {
            Enable();
        }
        
        private void OnShotByInvader(SpaceShip.EventResponse response)
        {
            Disable();
            MainBehaviour.OnCancelMove?.Invoke(new SpaceShip.EventResponse());
        }

        private void OnRespawn(SpaceShip.EventResponse response)
        {
            Enable();
        }
        

        private void HandleTurretInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var response = new SpaceShip.EventResponse()
                {

                };
                
                MainBehaviour.OnTurretShoot?.Invoke(response);
            }
        }
        
        private void HandleMovementInput()
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                var response = new SpaceShip.EventResponse()
                {
                    movementDirection = 1
                };
                
                MainBehaviour.OnPerformMove?.Invoke(response);
            }

            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                var response = new SpaceShip.EventResponse()
                {
                    movementDirection = -1
                };
                
                MainBehaviour.OnPerformMove?.Invoke(response);
            }
            
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                MainBehaviour.OnCancelMove?.Invoke(new SpaceShip.EventResponse());
            }
            
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                MainBehaviour.OnCancelMove?.Invoke(new SpaceShip.EventResponse());
            }
        }

        private void Enable()
        {
            enabled = true;
        }

        private void Disable()
        {
            enabled = false;
        }

        private void AddListeners()
        {
            Game.OnStarted += OnGameStart;

            if (MainBehaviour)
            {
                MainBehaviour.OnShotByInvader += OnShotByInvader;
                MainBehaviour.OnRespawn += OnRespawn;
            }
        }

        private void RemoveListeners()
        {
            Game.OnStarted -= OnGameStart;
            
            if (MainBehaviour)
            {
                MainBehaviour.OnShotByInvader -= OnShotByInvader;
                MainBehaviour.OnRespawn -= OnRespawn;
            }
        }
    }
}