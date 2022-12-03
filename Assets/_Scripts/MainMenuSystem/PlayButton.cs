using UnityEngine;

namespace MainMenuSystem
{
    public class PlayButton : MeshButton
    {
        private void Awake()
        {
            AddListeners();
        }

        private void Start()
        {
            Show();
        }
        
        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void OnMainMenuLoaded(MainMenu.EventResponse response)
        {
            transform.localScale = Vector3.one;
        }
        

        protected override void OnClicked()
        {
            base.OnClicked();
            Game.Load();
        }


        private void AddListeners()
        {
            MainMenu.OnLoaded += OnMainMenuLoaded;
        }

        private void RemoveListeners()
        {
            MainMenu.OnLoaded -= OnMainMenuLoaded;   
        }
    }
}