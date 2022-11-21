using UnityEngine;

namespace Utils.ModularBehaviour
{
    public abstract class BehaviourModule<T> : MonoBehaviour
        where T : MonoBehaviour, IMainBehaviour
    {
        protected virtual T MainBehaviour
        {
            get
            {
                if (!m_MainBehaviour)
                {
                    m_MainBehaviour = GetComponentInParent<T>();
                }

                return m_MainBehaviour;
            }
        }


        private T m_MainBehaviour;
    }
}