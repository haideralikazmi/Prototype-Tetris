using System;
using UnityEngine;
using UnityEngine.UI;

namespace HAK.UI.LevelProgression
{
    public class LevelProgressionViewRefs : MonoBehaviour
    {
        public Image BatteryFillImage;
        
        [Header("Buttons")]
        public Button OkayButton;
        public Button ReloadSceneButton;
        
        [Header("Views")]
        public GameObject CompletionScreen;
        public GameObject InGameScreen;
        public Battery[] LevelBatteries;
    }

}
