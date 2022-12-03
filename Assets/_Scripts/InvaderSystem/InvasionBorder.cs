using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class InvasionBorder : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private InvasionBorder pairBorder;


        private void Awake()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void OnTriggerEnter(Collider other)
        {
            CheckInvaderHit(other);
        }


        private void OnGameStarted(Game.EventResponse response)
        {
            Enable();
        }
        

        private void CheckInvaderHit(Collider other)
        {
            if (!other.TryGetComponent(out InvaderCollider invaderCollider)) return;
            
            if (invaderCollider is BonusInvaderCollider) return;

            pairBorder.Enable();
            Disable();
            invaderCollider.OnReachInvasionBorder();
        }
        
        private void Enable()
        {
            gameObject.SetActive(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void AddListeners()
        {
            Game.OnStarted += OnGameStarted;
        }

        private void RemoveListeners()
        {
            Game.OnStarted -= OnGameStarted;
        }
    }
}