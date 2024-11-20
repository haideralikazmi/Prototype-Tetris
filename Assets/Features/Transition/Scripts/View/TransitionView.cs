using System;
using HAK.Core;
using UnityEngine;

namespace HAK.UI.Transition
{
    public class TransitionView : BaseView
    {
        [SerializeField] private TransitionViewRefs _viewRefs;
        private ITransition _handler;
        
        public override void Initialize(object model)
        {
            _handler = model as ITransition;
            Register();
        }
        
        public override void Show()
        {
            base.Show();
            SetViewState(true);
        }
        
        public void SetProgressState(float progress,Action callback = null)
        {
            var fillDelta = Configs.ViewConfigs.HomeFillDelta;
            var fillAmount = Mathf.MoveTowards(_viewRefs.LoadingBarFillImage.fillAmount, progress,
                fillDelta * Time.deltaTime);
            _viewRefs.LoadingBarFillImage.fillAmount = fillAmount;
            if (fillAmount == 1f)
            {
                callback?.Invoke();
            }
        }

        public override void Hide()
        {
            base.Hide();
            SetViewState(false);
        }

        public bool IsTransitionComplete()
        {
            return true;
        }
    }
}

