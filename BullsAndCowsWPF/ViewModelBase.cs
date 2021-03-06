using System.CodeDom;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BullsAndCowsWPF.Annotations;

namespace BullsAndCowsWPF
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!field.Equals(value))
            {
                field = value;
                Fire(name);
            }
        }

        protected void Fire(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void Fire(params string[] names)
        {
            foreach (var name in names)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}