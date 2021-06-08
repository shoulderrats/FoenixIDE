namespace FoenixIDE.Processor
{
    public class RegisterAccumulator : Register
    {
        public int Value16
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
