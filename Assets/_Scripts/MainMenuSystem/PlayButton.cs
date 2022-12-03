namespace MainMenuSystem
{
    public class PlayButton : MeshButton
    {
        protected override void OnClicked()
        {
            base.OnClicked();
            Game.Load();
        }
    }
}