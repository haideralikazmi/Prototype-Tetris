using HAK.Core;
using HAK.Gameplay.Shape;
using HAK.UI.LevelProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HAK.Gameplay.LevelProgress
{ 
    public class LevelProgression : BaseGameplayModule, ILevelProgression 
    {
        
    [SerializeField] private LevelProgressionView _view;

 	public ITray TrayHandler { private get; set; }
    
    public override void Initialize()
    {
        _view.Initialize(this);
    }
    
    void ILevelProgression.CheckIfLevelCompleted()
    {
        if (TrayHandler.CheckIfAllShapesHaveBeenPlaced())
        {
            _view.ShowLevelCompleteScreen();
        }
    }

    void ILevelProgression.OnNextButtonClicked()
    {
        _view.HideLevelCompleteScreen();
        UpdateLevelCountInPref();
        LoadScene();
    }

    void ILevelProgression.OnReloadButtonClick()
    {
        UpdateLevelCountInPref();
        LoadScene();
    }

    int ILevelProgression.GetCurrentLevel()
    {
        var level = GetCurrentLevelCount();
        return level;
    }
            
    private void LoadScene()
    {
        SceneManager.LoadScene(Constants.SceneName.GameplayScene);
    }
    
    private void UpdateLevelCountInPref()
    {
        var currentLevel = GetCurrentLevelCount();
        currentLevel++;
        var levelCount = Configs.LevelConfigs.LevelData.Count;
        if (currentLevel >= levelCount)
        {
            currentLevel = 0;
        }
        SetLevel(currentLevel);
    }
            
    private int GetCurrentLevelCount()
    {
        var currentLevel = PlayerPrefs.GetInt(Constants.LevelPrefKeys.CurrentLevel, Configs.LevelConfigs.DefaultLevel);
        return currentLevel;
    }
    private void SetLevel(int level)
    {
        PlayerPrefs.SetInt(Constants.LevelPrefKeys.CurrentLevel, level);
    } 
    }
}

