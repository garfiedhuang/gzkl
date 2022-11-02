using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Cilent.UI.Models
{
     public class UserModel : ObservableObject
    {
        public string Index { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string BackColor { get; set; }

        public string Status { get; set; }

        public string UserType { get; set; }

        public bool isSelected =false;

        public bool IsSelected
        {
            get { return isSelected; }
            set{isSelected = value;RaisePropertyChanged(); }
        }

    }
}
