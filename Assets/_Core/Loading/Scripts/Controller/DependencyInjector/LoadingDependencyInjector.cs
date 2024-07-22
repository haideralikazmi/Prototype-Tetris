using HAK.Loading;
using UnityEngine;

namespace HAK.Core
{
    public class LoadingDependencyInjector : BaseDependencyInjector
    {
        [SerializeField] private LoadingController _loadingController;
        [SerializeField] private LoadingView _loadingView;
        public override void InjectDependencies()
        {
            _loadingController.ViewHandler = _loadingView;
            _loadingView.LoadingHandler = _loadingController;
        }
    }
}