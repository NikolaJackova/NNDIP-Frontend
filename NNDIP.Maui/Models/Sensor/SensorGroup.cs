using MvvmHelpers;
using NNDIP.ApiClient;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NNDIP.Maui.Models.Sensor
{
    public class SensorGroup : ObservableRangeCollection<SimpleDataDto>, INotifyPropertyChanged
    {
        public Sensor Sensor { get; set; }

        private string _groupIcon = Icons.DownArrow;
        public string GroupIcon
        {
            get => _groupIcon;
            set => SetProperty(ref _groupIcon, value);
        }

        public SensorGroup(Sensor sensor, List<SimpleDataDto> data) : base(data)
        {
            Sensor = sensor;
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
