using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        // button = GameObject.Find("Canvas/ButtonSummary/Button").GetComponent<Button>();
        // ボタンが選択された状態になる
        button.Select();
    }
}