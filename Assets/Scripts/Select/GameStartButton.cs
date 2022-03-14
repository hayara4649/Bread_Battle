using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameStartButton : MonoBehaviour
{
    [SerializeField] bool[] player=new bool[2];
    public void ActivateButton(int num)
    {
        player[num] = true;
        
        //プレイヤーが全てTrueならボタンを有効
        if(player.All(i => i))
        {
            gameObject.SetActive(true);
        }

        // if(player[0] && player[1])
        // {
        //     gameObject.SetActive(true);
        // }
        
    }

    public void OnGameStart() {
        SceneManager.LoadScene("BattleScene");
    }
}
