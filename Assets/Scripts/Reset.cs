using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    void Update()
    {
        Debug.Log(Keyboard.current);
        if(Keyboard.current.escapeKey.wasPressedThisFrame)  StartCoroutine(SceneReset());
    }

    IEnumerator SceneReset()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("TitleScene");
    }
}
