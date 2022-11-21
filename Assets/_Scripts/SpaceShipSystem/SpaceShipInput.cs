using UnityEngine;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipInput : BehaviourModule<SpaceShip>
    {
        private void Update()
        {
            HandleMovementInput();
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
    }
}