using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipHealthIcon : BehaviourModule<SpaceShipHealthBar>
    {
        private int Index => transform.GetSiblingIndex();


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
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
            var shouldActive = Index < healthLeft;
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