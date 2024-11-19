namespace HAK.Core.SpecialEffects
{
    public interface ISfx
    {
        void PlayLevelFailSfx();
        void LevelPassSfx();
        void OnShapePickUpFromBoard();
        void OnShapePlacementOnBoard();
    }
}