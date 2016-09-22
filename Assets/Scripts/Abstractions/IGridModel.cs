namespace Game.Abstractions
{
    public interface IGridModel<T> : IModel
    {
        event System.Action<int, int, T> TileRemovedEvent;

        int Rows { get; }
        int Columns { get; }
        int NumberOfTypes { get; }
        int NumberOfTiles { get; }
        T Get(int row, int column);
    }
}