using HAK.Core;
using HAK.UI.Transition;
using UnityEngine;

namespace HAK.Home
{
    public class HomeController : BaseModule ,IHome
    {
        [SerializeField] private HomeView _view;
        public ITransition TransitionHandler { private get; set; }

        public override void Initialize()
        {
            base.Initialize();
            _view.Initialize(this);
        }

        void IHome.OnPlayButtonClick()
        {
            TransitionHandler.TransitionToGameplay();
        }
    }
}
