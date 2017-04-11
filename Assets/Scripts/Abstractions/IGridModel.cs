namespace Game.Abstractions
{
    public interface IGridModel<T> : IModel
    {
        event System.Action<int, int, T> ElementAdded;
        event System.Action<int, int, T> ElementRemoved;

        int Rows { get; }
        int Columns { get; }
        int NumberOfTypes { get; }
        int NumberOfTiles { get; }
        T Get(int row, int column);
        GridElementModel<T>[] FindAll(T element);
        void Set(int row, int column, T data);
    }
}