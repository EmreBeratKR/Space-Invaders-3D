using SpaceShipSystem;

namespace InvaderSystem
{
    public class BonusInvaderCollider : InvaderCollider
    {
        protected new BonusInvader MainBehaviour
        {
            get
            {
                if (!m_MainBehaviour)
                {
                    m_MainBehaviour = GetComponentInParent<BonusInvader>();
                }

                return m_MainBehaviour;
            }
        }


        private BonusInvader m_MainBehaviour;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        public override void OnShotBySpaceShipBullet(SpaceShip spaceShip)
        {
            Collider.enabled = false;
            
            MainBehaviour.OnShotBySpaceShip?.Invoke(new BonusInvader.EventResponse());
        }
        
        public override void OnReachInvasionBorder()
        {
            
        }


        private void OnInitialized(BonusInvader.EventResponse response)
        {
            Collider.enabled = true;
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized += OnInitialized;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized -= OnInitialized;
            }
        }
    }
}