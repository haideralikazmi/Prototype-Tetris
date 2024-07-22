using UnityEngine;

namespace HAK.Core
{
    public class BaseModule : MonoBehaviour
    {
        public IGameFlow GameFlow;
        
        public void Register(IGameFlow gameFlow)
        {
            GameFlow = gameFlow;
        }
        
        public virtual void PreInitialize()
        {
            
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void PostInitialize()
        {
            
        }
    }
}