using System.Collections;
using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class UfoBonusInvader : BonusInvader
    {
        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float moveSpeed;
        [SerializeField] private float lifetime;


        private bool m_IsDead;
        

        private void Update()
        {
            Move();
        }


        public override void OnAfterInitialized()
        {
            base.OnAfterInitialized();
            SetLifetime();
        }


        private void OnDied_Internal(EventResponse response)
        {
            m_IsDead = true;
        }


        protected override void AddListeners()
        {
            base.AddListeners();
            OnDied += OnDied_Internal;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            OnDied -= OnDied_Internal;
        }


        private void Move()
        {
            if (m_IsDead) return;
            
            transform.position += Vector3.right * (Time.deltaTime * moveSpeed);
        }

        private void SetLifetime()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                m_IsDead = false;
                
                yield return new WaitForSeconds(lifetime);
                
                Release();
            }
        }
    }
}