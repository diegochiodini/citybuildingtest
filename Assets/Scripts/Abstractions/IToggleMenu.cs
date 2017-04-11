namespace Game.Abstractions
{
    public interface IToggleMenu
    {
        bool IsOpen { get; }

        void ToggleMenu();
    } 
}