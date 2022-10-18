namespace PayForXatu.MAUIApp.Models
{
    public class CounterValueModel : BindableBase
    {
        private double _value;

        public string Title { get; set; }
        public Guid CounterId { get; set; }
        public double Value
        {
            get { return _value; }
            set {SetProperty(ref _value, value);}
        }
    }
}
