namespace PayForXatu.MAUIApp.Models
{
    public class CounterNameModel : BindableBase
    {
        private string _name;

        public CounterNameModel()
        {
            Name = String.Empty;
        }

        public string Name
        {
            get { return _name; }
            set
            { SetProperty(ref _name, value); }
        }

        public Guid CounterId { get; set; }
    }
}
