using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    
    
    public void GotoSelect()
    {
        SceneManager.LoadScene("Select");
    }

    public void RetryBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
