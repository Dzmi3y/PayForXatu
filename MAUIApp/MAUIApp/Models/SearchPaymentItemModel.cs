using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.Models
{
    public class SearchPaymentItemModel : BindableBase
    {
        private bool _isSelected;

        public SearchPaymentItemModel()
        {
            ImageTapCommand = new Command(() => IsSelected = !IsSelected);
        }

        public string PaymentName { get; set; }

        public ICommand ImageTapCommand { get; set; }

        public Action Switched { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
                if (Switched != null)
                    Switched.Invoke();
            }
        }

    }

}
