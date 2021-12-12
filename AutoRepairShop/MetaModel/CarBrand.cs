using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRepairShop 
{// Здесь не будет Nullable Id и Setters, так как марки и модели машин мы всегда берем из базы и не даем менять другим способом. Только через базу.
    public class CarBrand : INotifyPropertyChanged
    {// Здесь не будет Nullable Id так как марки и модели машин мы всегда знаем точно на момент старта программы.
        private string _brandName;
        public int Id { get; }
        public string BrandName
        {
            get
            {
                return _brandName;
            }
            set
            {
                _brandName = value;
                OnPropertyChanged(nameof(BrandName));

            }

        }

        public CarBrand(int id, string brandName)
        {
            this.Id = id;
            this.BrandName = brandName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return BrandName;
        }
    }
}
