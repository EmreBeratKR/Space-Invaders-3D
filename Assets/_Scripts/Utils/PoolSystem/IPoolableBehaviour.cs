using UnityEngine;

namespace Utils.PoolSystem
{
    public interface IPoolableBehaviour<T> 
        where T : MonoBehaviour, IPoolableBehaviour<T>
    {
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void SetParent(Transform parent);
        void Release();
        void Inject(BehaviourPool<T> pool);
        void SetActive(bool isActive);
        void OnReset();
        void OnBeforeInitialized();
        void OnAfterInitialized();
    }
}