using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, bool validateSceneName = true, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(name, validateSceneName, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string name, bool validateSceneName, Action onLoaded = null)
        {
            if (validateSceneName && SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(name);

            loadOperation.allowSceneActivation = false;
            yield return new WaitForSeconds(0.2f);
            loadOperation.allowSceneActivation = true;

            onLoaded?.Invoke();
        }
    }
}