using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectCharacter
{
    public class GameManager : MonoBehaviour
    {
        // [SerializeField] static GameManager gameManager;
        // [SerializeField] GameManagerDate gameManagerDate = null;
        // private void Awake() {
        //     //ひとつだけのGameManager
        //     if(gameManager == null)
        //     {
        //         gameManager = this;
        //         DontDestroyOnLoad(this);
        //     } else {
        //         Destroy(gameObject);
        //     }
        // }

        // public GameManagerDate GetGameManagerData() 
        // {
        //     return gameManagerDate;
        // }

        private static GameManager gameManager;
        //　ゲーム全体で管理するデータ
        [SerializeField]
        private GameManagerDate gameManagerData = null;

        private void Awake() {
            //　世界に一つだけのMyGameManagerにする処理
            if (gameManager == null) {
                gameManager = this;
                DontDestroyOnLoad(this);
            } else {
                Destroy(gameObject);
            }
        }
        //　MyGameManagerDataを返す
        public GameManagerDate GetGameManagerData() {
            return gameManagerData;
        }
    }

}