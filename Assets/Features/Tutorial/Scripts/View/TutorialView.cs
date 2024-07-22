using HAK.UI;
using UnityEngine;

namespace HAK.Core.Tutorial
{
    public class TutorialView : BaseView
    {
        [SerializeField] private TutorialViewRefs _viewRefs;
        private ITutorial _handler;
        
        public override void Initialize(object model = null)
        {
            base.Initialize(model);
            _handler = model as ITutorial;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void MoveHandToTarget(Vector3 targetPosition, Vector3 startingPosition)
        {
            
        }

        private void EnableOverlay()
        {
            _viewRefs.enabled = true;
        }

        private void DisableOverlay()
        {
            _viewRefs.Overlay.enabled = false;
        }

        private void DisableHand()
        {
            _viewRefs.HandIcon.SetActive(false);
        }

        private void EnableHand()
        {
            _viewRefs.HandIcon.SetActive(true);
        }
    }
}