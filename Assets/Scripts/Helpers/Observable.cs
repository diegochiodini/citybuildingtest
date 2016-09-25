public class Observable<T>
{
    public event System.Action<T> OnChange;

    private T _value;
    public T Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            if (OnChange != null)
            {
                OnChange(_value);
            }
        }
    }

    public Observable(T value)
    {
        Value = value;
    }
}