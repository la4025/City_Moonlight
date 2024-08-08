using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    [SerializeField]
    LoadSceneMode loadSceneMode;
    [SerializeField]
    UnityEvent onLoadAsync;
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    public void LoadSceneAsync()
    {
        SceneManager.LoadSceneAsync(sceneName, loadSceneMode).completed += (op) => { onLoadAsync.Invoke(); };
    }

}
