public class Observer<T>
{
    public delegate void ValueChangedDelegate(T prevValue, T newValue);
    public event ValueChangedDelegate ValueChanged;

    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value.Equals(value))
                return;

            T buffer = value;
            this.value = value;

            ValueChanged?.Invoke(buffer, value);
        }
    }

    private T value;

    public Observer(T value = default)
    {
        this.value = value;
    }
}