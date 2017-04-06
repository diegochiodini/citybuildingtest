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
}