using System.Collections.Generic;
using System.Configuration;

namespace AutoRepairShop.Utils
{
    public class SettingsManager
    {
        #region Fields
        private static SettingsManager _settingsManager;
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        #endregion Fields

        #region Constructors

        private SettingsManager()
        {
            if (Properties.Settings.Default.Properties.Count > 0)
            {
                foreach(SettingsProperty setting in Properties.Settings.Default.Properties)
                {
                    _settings.Add(setting.Name, setting.DefaultValue.ToString());
                }
            }
        }
        #endregion Constructors

        public static SettingsManager getSettingsManager()
        {
            if (_settingsManager == null)
            {
                _settingsManager = new SettingsManager();
            }
            return _settingsManager;
        }

        public Dictionary<string, string> Settings
        {
            get { return _settings; }
        }

        public void saveSettings()
        {
            foreach (var setting in Settings)
            {
                Properties.Settings.Default[setting.Key] = setting.Value;
            }
            Properties.Settings.Default.Save();
        }
    }
}
