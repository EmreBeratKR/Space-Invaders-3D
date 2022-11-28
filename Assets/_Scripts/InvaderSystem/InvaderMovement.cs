using UnityEngine;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderMovement : BehaviourModule<Invader>
    {
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnInvasionStep(Invader.EventResponse response)
        {
            Move(response.invasionMovement);
        }

        private void OnInvadeLowerCommand(Invader.EventResponse response)
        {
            Move(response.invadeLowerMovement);
        }


        private void Move(Vector3 movement)
        {
            MainBehaviour.Position += movement;
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInvasionStep += OnInvasionStep;
                MainBehaviour.OnInvadeLowerCommand += OnInvadeLowerCommand;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInvasionStep -= OnInvasionStep;
                MainBehaviour.OnInvadeLowerCommand -= OnInvadeLowerCommand;
            }
        }
    }
}