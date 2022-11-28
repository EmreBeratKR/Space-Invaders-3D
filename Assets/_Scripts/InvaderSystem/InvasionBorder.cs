using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class InvasionBorder : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private InvasionBorder pairBorder;
        
        
        private void OnTriggerEnter(Collider other)
        {
            CheckInvaderHit(other);
        }
        

        private void CheckInvaderHit(Collider other)
        {
            if (!other.TryGetComponent(out InvaderCollider invaderCollider)) return;

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
    }
}