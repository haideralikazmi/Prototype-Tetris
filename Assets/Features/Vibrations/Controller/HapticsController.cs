public class HapticsController
{
    public void InitializeVibrations()
    {
        Vibration.Init();
    }

    public void PlayLightImpact()
    {
#if UNITY_IOS
        Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
#endif
    }

    public void PlayMediumImpact()
    {
#if UNITY_IOS
        Vibration.VibrateIOS(ImpactFeedbackStyle.Medium);
#endif
    }
    
    public void PlayHeavyImpact()
    {
#if UNITY_IOS
        Vibration.VibrateIOS(ImpactFeedbackStyle.Heavy);
#endif
    }
    
    public void PlayRigidImpact()
    {
#if UNITY_IOS
        Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);
#endif
    }
    
    public void PlaySoftImpact()
    {
#if UNITY_IOS
        Vibration.VibrateIOS(ImpactFeedbackStyle.Soft);
#endif
    }
}