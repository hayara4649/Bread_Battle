using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class TitleToMain : MonoBehaviour
{
    public Fade fade;
    [SerializeField] string sceneName;
    // public GameObject ImgEnd;



    // Update is called once per frame
    void Update()
    {
        Debug.Log(Keyboard.current);
        //スペースキー押したらシーン移行
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            // ImgEnd.SetActive(true);
            OnNextScene();
            Debug.Log("Click");
        }
    }
    //ゲームスタート
    public void OnNextScene()
    {
        // if (ImgEnd.activeSelf)
        // {
        //     return;
        // }

        //トランジションを掛けてシーン遷移する
        // fade.FadeIn(時間, () => 完了したときにやりたいこと)
        fade.FadeIn(1f, () => SceneManager.LoadScene(sceneName));
    }
}
