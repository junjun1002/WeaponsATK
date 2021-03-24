using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのフェードを行うクラス
/// </summary>
public class SceneLoder : SingletonMonoBehavior<SceneLoder>
{
    protected override void Awake()
    {
        base.Awake();
       // DontDestroyOnLoad(gameObject);
    }

    public void Load(string sceneName)
    {    
        SceneManager.LoadSceneAsync(sceneName);    
    }
}
