using System.Collections;
using HAK.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HAK.UI.Transition
{
    public class Transition : BaseModule, ITransition
    {
        [SerializeField] private TransitionView _view;
        private AsyncOperation _sceneLoadingOperation;
        private float _target;
        
        void ITransition.TransitionToGameplay()
        {
            _view.Show();
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
                _target =  Mathf.Clamp01(_sceneLoadingOperation.progress/ Configs.ViewConfig.HomeBarthreshold);
                _view.SetProgressState(_target, ActivateScene);
                yield return null;
            }
        }
        private void ActivateScene( )
        {
            _sceneLoadingOperation.allowSceneActivation = true;
        }
    }
}