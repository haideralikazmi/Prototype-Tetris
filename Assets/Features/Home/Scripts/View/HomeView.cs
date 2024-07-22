using HAK.Home;
using UnityEngine;
using HAK.UI;

public class HomeView : BaseView, IHomeView
{
    [SerializeField] private HomeViewRefs _viewRefs;

    private IHome _handler;
    
    public override void Initialize(object model)
    {
        base.Initialize(model);
        _handler = model as IHome;
    }

    public override void Register()
    {
        base.Register();
        _viewRefs.PlayButton.onClick.AddListener(OnPlayButtonClick);
        _viewRefs.SettingsButton.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnSettingsButtonClick()
    {
        
    }

    public override void Unregister()
    {
        _viewRefs.SettingsButton.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClick()
    {
        _handler.OnPlayButtonClick();
    }
}
