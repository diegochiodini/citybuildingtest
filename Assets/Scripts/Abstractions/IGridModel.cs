namespace Game.Abstractions
{
    public class GridElement<T>
    {
        public int Row;
        public int Column;
        public T Element;

        public override bool Equals(System.Object obj)
        {
            GridElement<T> element = obj as GridElement<T>;
            if (element == null)
            {
                return false;
            }
            return AreEqual(element);
        }

        public bool Equals(GridElement<T> element)
        {
            if (element == null)
            {
                return false;
            }

            return AreEqual(element);
        }

        private bool AreEqual(GridElement<T> element)
        {
            bool hasSamePosition = this.Column == element.Column && this.Row == element.Row;

            return hasSamePosition && (
                (this.Element == null && element.Element == null)
                || this.Element.Equals(element.Element));
        }

        public override int GetHashCode()
        {
            return Column ^ Row;
        }
    }

    public interface IGridModel<T> : IModel
    {
        event System.Action<int, int, T> ElementAdded;
        event System.Action<int, int, T> ElementRemoved;

        int Rows { get; }
        int Columns { get; }
        int NumberOfTypes { get; }
        int NumberOfTiles { get; }
        T Get(int row, int column);
        GridElement<T>[] FindAll(T element);
        void Set(int row, int column, T data);
    }
}