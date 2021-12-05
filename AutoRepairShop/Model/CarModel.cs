using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRepairShop
{
    public class CarModel : INotifyPropertyChanged
    {// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
        private string _model;
        public int Id { get; }
        public CarBrand CarBrand { get; }
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public CarModel(int id, CarBrand carBrand, string model)
        {
            Id = id;
            CarBrand = carBrand;
            Model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //public override string ToString()
        //{
        //    return Model;
        //}
    }
}
