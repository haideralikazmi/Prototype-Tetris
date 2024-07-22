
namespace HAK.Gameplay.LevelProgress
{
    public interface ILevelProgression
    {
        void IncreaseBatteryHealth();
        void DecreaseBatteryHealth();
        void OnNextButtonClicked();
        void OnReloadButtonClick();
        int GetCurrentLevel();
        void CheckIfLevelCompleted();
    }
}
