using System.Collections;
using ScoreSystem;
using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class UfoBonusInvader : BonusInvader
    {
        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float moveSpeed;
        [SerializeField] private float targetPositionX;


        private int CurrentScore => ScoreManager.CurrentUfoScore;
        

        private bool m_IsDead;
        

        private void Update()
        {
            Move();
        }


        public override void OnAfterInitialized()
        {
            base.OnAfterInitialized();
            m_IsDead = false;
        }


        private void OnDied_Internal(EventResponse response)
        {
            m_IsDead = true;
            ScoreManager.EarnScore(CurrentScore);
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
            
            if (transform.position.x < targetPositionX) return;
            
            Release();
        }
    }
}