using AutoRepairShop.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoRepairShop.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private RelayCommand _selectDbFileCommand;
        private RelayCommand _okCommand;

        private SettingsManager settingsManager = SettingsManager.getSettingsManager();
        private string _dbPathString;
        #endregion Fields

        #region Constructors
        public SettingsViewModel()
        {
            // Нужно ли проверять наличие данных в настройках?
            _dbPathString = settingsManager.Settings[Constants.CONNECTION_STRING];
        }
        #endregion Constructors

        public string DbPathString
        {
            get { return _dbPathString; }
            set 
            {
                _dbPathString = value;
                OnPropertyChanged(nameof(DbPathString));
            }
        }

        #region Commands
        /// <summary>
        /// Returns the command used to save user settings.
        /// </summary>
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(OkButtonClicked);
                }
                return _okCommand;
            }
        }
        private void OkButtonClicked(object commandParameter)
        {
            settingsManager.Settings[Constants.CONNECTION_STRING] = _dbPathString;
            settingsManager.saveSettings();
        }
        
        public ICommand SelectDbFileCommand
        {
            get
            {
                if (_selectDbFileCommand == null)
                {
                    _selectDbFileCommand = new RelayCommand(SelectDbFile);
                }
                return _selectDbFileCommand;
            }
        }

        private void SelectDbFile(object commandParameter)
        {
            OpenFileDialog findDatabase = new OpenFileDialog();
            findDatabase.Filter = "Файлы баз данных MS Access|*.accdb";
            findDatabase.Multiselect = false;
            findDatabase.CheckFileExists = true;
            findDatabase.CheckPathExists = true;
            findDatabase.Title = "Укажите файл базы данных";

            if (findDatabase.ShowDialog() == DialogResult.OK)
            {
                DbPathString = findDatabase.FileName;
            }
        }

        #endregion Commands

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
