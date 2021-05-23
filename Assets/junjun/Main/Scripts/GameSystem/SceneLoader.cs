using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのフェードを行うクラス
/// </summary>
public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
       // DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// シーンをロードさせる関数
    /// </summary>
    /// <param name="sceneName"></param>
    public void Load(string sceneName)
    {    
        SceneManager.LoadSceneAsync(sceneName);    
    }
}
