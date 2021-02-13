using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoder : SingletonMonoBehavior<SceneLoder>
{
    //[SerializeField] OVRScreenFade m_screenFade;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //m_screenFade = gameObject.GetComponent<OVRScreenFade>();
    }
    public void LodeTitle()
    {
        //m_screenFade.FadeIn();
        SceneManager.LoadSceneAsync("GameScene");
    }
}
