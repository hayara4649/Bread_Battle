
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SelectCharacter
{
    public class ChooseCharacter : MonoBehaviour
    {
        [SerializeField] GameObject gameManager;
        [SerializeField] GameManagerDate gameManagerDate;
        [SerializeField] GameObject gameStartButton;
        public GameObject currentSelect;
        [SerializeField] int buttonNum=0;


        void Start()
        {
            gameManager = GameObject.Find("GameManager");

            //GameManagerDataを取得
            //ここが上手くいっていない
            // gameManagerDate = FindObjectOfType<GameManager>().GetGameManagerData();
            gameManagerDate = gameManager.GetComponent<GameManagerDate>();
            Debug.Log(gameManagerDate);

            
            if(!gameStartButton) return;
            //ボタンを取得
            // gameStartButton = GameObject.Find("ButtonPanel/GameStart").gameObject;
            //ボタンを無効にする
            gameStartButton.SetActive(false);
        }

        //キャラクターを選択した時に実行しキャラクターデータをMyGameManagerDataにセット
        public void OnSelectCharacter(GameObject character)
        {

            //　MyGameManagerDataにキャラクターデータをセットする
            if(buttonNum == 0)  gameManagerDate.SetCharacter(character);
            if(buttonNum == 1)  gameManagerDate.SetCharacter_2(character);

            
            //表示用に現在選んでいるキャラを保存
            currentSelect = character;

            if(!gameStartButton) return;

            //GameStartButtonにプレイヤーが選択されたことを知らせる
            gameStartButton.GetComponent<GameStartButton>().ActivateButton(buttonNum);

            // //　ゲームスタートボタンを有効にする
            // gameStartButton.SetActive(true);
        }

        //　キャラクターを選択した時に背景をオンにする
        public void SwitchButtonBackground(int buttonNumber)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == buttonNumber - 1)
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(false);
                }
            }
        }
    }
}