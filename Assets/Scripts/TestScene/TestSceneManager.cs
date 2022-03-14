using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneManager : MonoBehaviour {
    // 各プレイヤーのPrefabを設定する
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    void Awake() {
        // 各プレイヤーのスポーン位置を取得する
        GameObject player1Spawn = GameObject.Find("Player1Spawn");
        GameObject player2Spawn = GameObject.Find("Player2Spawn");

        // 各プレイヤーを生成し、番号を振る
        GameObject player1 = Instantiate(player1Prefab, player1Spawn.transform.position, Quaternion.identity);

        PlayerController player1Controller = player1.GetComponent<PlayerController>();
        player1Controller.SetPlayerNum(1);
        player1Controller.SetDirection(1.0f);

        GameObject player2 = Instantiate(player2Prefab, player2Spawn.transform.position, Quaternion.identity);
        // player2.transform.localScale = new Vector3(1, 1, 1);

        PlayerController player2Controller = player2.GetComponent<PlayerController>();
        player2Controller.SetPlayerNum(2);
        player2Controller.SetDirection(-1.0f);

        // 各InputManagerにプレイヤーオブジェクトを渡す
        KeyboardInputManager keyboardInputManager = GameObject.Find("KeyboardInput").GetComponent<KeyboardInputManager>();
        keyboardInputManager.SetPlayer(player1);

        GamepadInputManager gamepadInputManager = GameObject.Find("GamepadInput").GetComponent<GamepadInputManager>();
        gamepadInputManager.SetPlayer(player2);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }
}
