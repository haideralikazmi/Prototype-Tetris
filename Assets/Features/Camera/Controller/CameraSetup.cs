using HAK.Gameplay;
using UnityEngine;

public class CameraSetup : BaseGameplayModule, IBaseCamera
{
    private Camera _mainCamera;

    public override void PreInitialize()
    {
        SetMainCamera();
    }

    private void SetMainCamera()
    {
        _mainCamera = Camera.main;
    }

    Camera IBaseCamera.GetMainCamera()
    {
        return _mainCamera;
    }
}