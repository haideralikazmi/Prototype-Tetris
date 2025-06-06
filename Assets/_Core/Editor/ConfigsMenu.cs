using log4net.Core;
using UnityEngine;
using UnityEditor;

namespace HAK.Core
{
    public class ConfigsMenu: MonoBehaviour
    {
        [MenuItem("HAK/Configs/AppConfig")]
        private static void ShowAppConfig()
        {
            var appConfig = Resources.Load<AppConfigs>(Constants.ConfigPath.AppConfigPath);
            if (appConfig != null)
            {
                Selection.activeObject = appConfig;
                return;
            }
            Debug.LogError("AppConfig asset not found!");
        }
        
        [MenuItem("HAK/Configs/GameConfig")]
        private static void ShowGameConfig()
        {
            var appConfig = Resources.Load<GameConfigs>(Constants.ConfigPath.GameConfigsPath);
            if (appConfig != null)
            {
                Selection.activeObject = appConfig;
                return;
            }
            Debug.LogError("GameConfig asset not found!");
        }
        
        [MenuItem("HAK/Configs/ViewConfig")]
        private static void ShowViewConfig()
        {
            var appConfig = Resources.Load<ViewConfigs>(Constants.ConfigPath.ViewConfigsPath);
            if (appConfig != null)
            {
                Selection.activeObject = appConfig;
                return;
            }
            Debug.LogError("ViewConfig asset not found!");
        }
        
        [MenuItem("HAK/Configs/LevelConfig")]
        private static void ShowLevelConfig()
        {
            var levelConfig = Resources.Load<LevelConfigs>(Constants.ConfigPath.LevelConfigsPath);
            if (levelConfig != null)
            {
                Selection.activeObject = levelConfig;
                return;
            }
            Debug.LogError("LevelConfig asset not found!");
        }
    }
}