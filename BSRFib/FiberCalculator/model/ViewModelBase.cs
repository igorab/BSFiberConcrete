using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    public interface IViewModel
    {
                void OnPropertyChanged(string prop);
    }
                public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
                public event PropertyChangedEventHandler PropertyChanged;
                                public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
            }
}
