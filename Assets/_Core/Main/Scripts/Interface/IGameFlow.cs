using UnityEngine;

namespace HAK.Core
{
    public interface IGameFlow
    { 
        AsyncOperation OnPlay();
    }
}