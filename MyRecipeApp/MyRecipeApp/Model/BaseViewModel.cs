using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyRecipeApp.Model
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetValue<T>(ref T backingProperty, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingProperty, value))
            {
                return false;
            }

            backingProperty = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
