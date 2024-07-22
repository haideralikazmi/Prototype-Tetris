using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HAK.Core
{
    public class GameFlow : MonoBehaviour, IGameFlow
    {
        [SerializeField] private BaseModule[] _modules;
        private AsyncOperation _sceneLoadingOperation;
        
        private void Awake()
        {
           for(var i = 0; i<_modules.Length; i++)
            {
                _modules[i].Register(this);
            }
        }

        private void OnEnable()
        {
            for(var i = 0; i<_modules.Length; i++)
            {
                _modules[i].PreInitialize();
            }
        }
        
        private void Start()
        {
            for(var i = 0; i<_modules.Length; i++)
            {
                _modules[i].Initialize();
            }
            OnInitializationComplete();
        }
        
        private void OnInitializationComplete()
        {
            StartCoroutine(PostInitialize());
            IEnumerator PostInitialize()
            {
                yield return null;
                for(var i = 0; i<_modules.Length; i++)
                {
                    _modules[i].PostInitialize();
                }
            }
        }

        public AsyncOperation OnPlay()
        {
            var operation = LoadGameplayScene();
            return operation;
        }

        private AsyncOperation LoadGameplayScene()
        {
            _sceneLoadingOperation = SceneManager.LoadSceneAsync(Constants.SceneName.MainScene);
            _sceneLoadingOperation.allowSceneActivation = false;
            return _sceneLoadingOperation;
        }
    }
}