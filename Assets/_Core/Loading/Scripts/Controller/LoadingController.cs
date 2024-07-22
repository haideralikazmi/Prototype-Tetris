using System;
using System.Collections;
using System.Threading.Tasks;
using HAK.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HAK.Loading
{
    public class LoadingController : MonoBehaviour, ILoading
    {
        public ILoadingView ViewHandler { private get; set; }
        
        private AsyncOperation _sceneLoadingOperation;
        private float _target;

        private void Start()
        {
            ViewHandler.Initialize();
            LoadScene();
        }
        
        private void LoadScene()
        {
            _sceneLoadingOperation = SceneManager.LoadSceneAsync(Constants.SceneName.GameplayScene);
            _sceneLoadingOperation.allowSceneActivation = false;
            StartCoroutine( SetProgressState());
        }
        private IEnumerator SetProgressState()
        {
            while (!_sceneLoadingOperation.isDone)
            {
               _target =  Mathf.Clamp01(_sceneLoadingOperation.progress/ Configs.ViewConfig.LoadingBarthreshold);
               ViewHandler.SetProgressState(_target, Initialize);
                yield return null;
            }
        }

        void Initialize( )
        {
            _sceneLoadingOperation.allowSceneActivation = true;
        }
    }
}