using System;
using HAK.Core;
using UnityEngine;

namespace HAK.Loading
{
    public class LoadingView : MonoBehaviour, ILoadingView
    {
        [SerializeField] private LoadingViewRefs _viewRefs;
        
        public ILoading LoadingHandler { private get; set; }
        
        void ILoadingView.Initialize()
        {
            _viewRefs.loadingBarFillImage.fillAmount = 0;
        }

        void ILoadingView.SetProgressState(float progress,Action callback = null)
        {
            var fillDelta = Configs.ViewConfig.LoadingFillDelta;
            
            var fillAmount = Mathf.MoveTowards(_viewRefs.loadingBarFillImage.fillAmount, progress,
                fillDelta * Time.deltaTime);
            _viewRefs.loadingBarFillImage.fillAmount = fillAmount;
            if (fillAmount == 1f)
            {
                callback?.Invoke();
            }
        }
    }
}