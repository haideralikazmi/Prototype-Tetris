
namespace HAK.Gameplay.LevelProgress
{
    public interface ILevelProgression
    {
        void OnNextButtonClicked();
        void OnReloadButtonClick();
        int GetCurrentLevel();
        void CheckIfLevelCompleted();
    }
}
