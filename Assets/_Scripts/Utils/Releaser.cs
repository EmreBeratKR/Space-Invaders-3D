using UnityEngine;
using Utils.PoolSystem;

namespace Utils
{
    public abstract class Releaser<T> : MonoBehaviour
        where T : IReleasable
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckTHit(other);
        }
        

        protected virtual void OnReleased(T obj)
        {
            obj.Release();
        }

        
        private void CheckTHit(Collider other)
        {
            if (!other.TryGetComponent(out T otherAsT)) return;
            
            OnReleased(otherAsT);
        }
    }
}