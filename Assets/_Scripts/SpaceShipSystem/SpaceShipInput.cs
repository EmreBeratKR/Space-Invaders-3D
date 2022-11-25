using UnityEngine;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipInput : BehaviourModule<SpaceShip>
    {
        private void Awake()
        {
            AddListeners();
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


        private void OnShotByInvader(SpaceShip.EventResponse response)
        {
            enabled = false;
            MainBehaviour.OnCancelMove?.Invoke(new SpaceShip.EventResponse());
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

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnShotByInvader += OnShotByInvader;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnShotByInvader -= OnShotByInvader;
            }
        }
    }
}