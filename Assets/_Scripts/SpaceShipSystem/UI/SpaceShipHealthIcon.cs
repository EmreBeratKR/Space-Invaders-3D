using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipHealthIcon : BehaviourModule<SpaceShipHealthBar>
    {
        private int Index => transform.GetSiblingIndex();


        private void Awake()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void OnBarChanged(SpaceShipHealthBar.EventResponse response)
        {
            var healthLeft = response.healthLeft;
            HandleState(healthLeft);
        }


        private void HandleState(int healthLeft)
        {
            var shouldActive = Index < healthLeft - 1;
            gameObject.SetActive(shouldActive);
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnChanged += OnBarChanged;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnChanged -= OnBarChanged;
            }
        }
    }
}