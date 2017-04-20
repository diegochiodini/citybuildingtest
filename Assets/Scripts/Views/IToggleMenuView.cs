namespace Game.Abstractions
{
    public interface IToggleMenuView
    {
        bool IsOpen { get; }

        void ToggleMenu();
    } 
}