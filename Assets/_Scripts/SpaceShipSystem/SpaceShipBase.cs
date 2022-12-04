using InvaderSystem;
using UnityEngine;

namespace SpaceShipSystem
{
    public class SpaceShipBase : MonoBehaviour, ITriggerEnterByInvader
    {
        public const float SafeAreaHeight = -37.5f;
        
        
        public void TriggerEnter()
        {
            Game.RaiseGameOver(Game.GameOverReason.InvaderReachedBase);
        }
    }
}