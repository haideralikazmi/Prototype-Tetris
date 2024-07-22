using UnityEngine;

namespace HAK.UI
{
    public class BaseView : MonoBehaviour
    {
        public virtual void Initialize(object model=null)
        {
            Register();
        }
        
        public virtual void Show()
        {
            SetViewState(true);
        }

        public virtual  void Register()
        {
            
        }
        
        public virtual void Unregister()
        {
            
        }
        
        public virtual void Hide()
        {
            Unregister();
            SetViewState(false);
        }

        public void SetViewState(bool state) => gameObject.SetActive(state);

    }
}

