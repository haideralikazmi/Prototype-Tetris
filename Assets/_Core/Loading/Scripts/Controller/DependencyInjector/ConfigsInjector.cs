using HAK.Core;
using UnityEngine;

public class ConfigsInjector : MonoBehaviour
{
    [SerializeField] private AppConfig _appConfigs;
    [SerializeField] private GameConfigs gameConfigs;
    [SerializeField] private ViewConfigs viewConfigs;
    [SerializeField] private LevelConfigs levelConfigs;

    private void Awake()
    {
        InjectConfigs();
    }

    public void InjectConfigs()
    {
        Configs.AppConfig = _appConfigs;
        Configs.GameConfigs = gameConfigs;
        Configs.ViewConfigs = viewConfigs;
        Configs.LevelConfigs = levelConfigs;
    }
}
