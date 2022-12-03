using InvaderSystem;
using UnityEngine;

namespace SpaceShipSystem
{
    public class SpaceShipBase : MonoBehaviour, ITriggerEnterByInvader
    {
        public void TriggerEnter()
        {
            Game.RaiseGameOver(Game.GameOverReason.InvaderReachedBase);
        }
    }
}