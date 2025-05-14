using HAK.Core;
using UnityEngine;

public class ConfigsInjector : MonoBehaviour
{
    [SerializeField] private AppConfigs _appConfigs;
    [SerializeField] private GameConfigs _gameConfigs;
    [SerializeField] private ViewConfigs _viewConfigs;
    [SerializeField] private LevelConfigs _levelConfigs;

    private void Awake()
    {
        InjectConfigs();
    }

    public void InjectConfigs()
    {
        Configs.AppConfigs = _appConfigs;
        Configs.GameConfigs = _gameConfigs;
        Configs.ViewConfigs = _viewConfigs;
        Configs.LevelConfigs = _levelConfigs;
    }
}
