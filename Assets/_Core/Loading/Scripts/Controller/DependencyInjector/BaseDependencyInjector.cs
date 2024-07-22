using UnityEngine;

namespace HAK.Core
{
    public class BaseDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            InjectDependencies();
        }

        public virtual void InjectDependencies()
        {
            
        }
    }
}