using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // ガード関係
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private float guardingRatio; // ガード時にダメージを何割にするか
    private GameObject barrier; // バリアのオブジェクトを入れる
    private bool isGuarding;

    private Slider slider;
    private BattleFinish battle;

    private Rigidbody2D rb; // Rigidbodyコンポーネント
    protected Animator animator; // Animatorコンポーネント
    private SpriteRenderer spriteRenderer;

    // キャラクターステータス関連
    [SerializeField] private int maxHp = 50; // 最大HP
    [SerializeField] private float moveSpeed = 3; // 移動速度
    [SerializeField] private float jumpForce = 200.0f; // ジャンプのときにかかる力

    // ステータス状態
    private int hp; // 現在のHP

    public bool canMove; // 移動可能かどうか。アニメーションから操作する
    private bool isJumping; // ジャンプ中かどうか
    private float freezingTime; // 硬直時間。0以上のときはなにもできない

    // サウンド関連
    [SerializeField] AudioClip[] clips; // 0:攻撃くらったとき, 1:スキル, 2:ガード時の被ダメ
    private AudioSource audioSource;

    [SerializeField] int playerNum;

    protected void Start()
    {
        switch (playerNum)
        {
            case 1:
                slider = GameObject.Find("Canvas").transform.Find("Player1_HP").GetComponent<Slider>();
                break;
            case 2:
                slider = GameObject.Find("Canvas").transform.Find("Player2_HP").GetComponent<Slider>();
                break;
            default:
                // デバッグ用
                this.playerNum = 1;
                slider = GameObject.Find("Canvas").transform.Find("Player1_HP").GetComponent<Slider>();
                break;
        }

        slider.maxValue = this.maxHp;
        this.hp = this.maxHp;
        slider.value = this.hp;

        // コンポーネントを取得する
        this.rb = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        // フラグを初期化する
        this.canMove = true;
        this.isJumping = false;
        this.isGuarding = false;

        //シーン移行用のインスタンス
        battle = GameObject.FindObjectOfType<BattleFinish>();

        //AudioSourse取得
        audioSource = GetComponent<AudioSource>();
    }

    protected void Update() {
        // 硬直時間を減らす
        if (this.freezingTime > 0) {
            this.animator.SetBool("IsFreezing", true);
            this.freezingTime -= Time.deltaTime;
            if (this.freezingTime <= 0) this.spriteRenderer.color = new Color(255.0f, 255.0f, 255.0f);
        }
        else this.animator.SetBool("IsFreezing", false);

        if (this.isJumping) this.animator.SetFloat("YSpeed", this.rb.velocity.y);
    }

    // 向きを変える
    public void SetDirection(float direction) {
        // 現在の角度を取得する
        Vector3 worldAngle = this.transform.eulerAngles;

        // 向いている方向と違う方向が渡されたとき
        if ((int)worldAngle.y == 0 && direction > 0 || (int)worldAngle.y == 180 && direction < 0) {
            this.transform.Rotate(0, 180f, 0); // 180度回転
        }
    }

    // どんなときでも1フレームに1回呼ばれる。xにはx軸方向の入力が渡される
    public void OnMove(float x)
    {
        // 移動禁止中、もしくは硬直中は速度を0にする
        if (!this.canMove || this.isGuarding || this.freezingTime > 0) x = 0;

        // ジャンプ中は向きを変えない
        if (!isJumping) this.SetDirection(x);

        this.animator.SetFloat("Speed", Mathf.Abs(x));
        this.rb.velocity = new Vector2(x * this.moveSpeed, this.rb.velocity.y);
    }

    // ジャンプキーが押されたときに呼ばれる
    public void OnJump()
    {
        // ジャンプ中はジャンプできない
        if (this.isJumping || this.isGuarding || !this.canMove || this.freezingTime > 0) return;

        this.animator.SetTrigger("Jump");
    }

    // 着地するときにFootPointから呼ばれる
    public void OnGround()
    {
        if (this.isJumping) {
            this.animator.SetTrigger("OnGround");
            this.isJumping = false;
        }
    }
    
    // Jumpアニメーション中に呼び出す
    public void Jump() {
        this.rb.AddForce(this.transform.up * this.jumpForce);

        this.isJumping = true;
    }

    // Attack1ボタンが押されたときに呼ばれる
    public void OnAttack1()
    {
        if (this.isJumping || this.isGuarding || !this.canMove || this.freezingTime > 0) return;

        this.animator.SetTrigger("Attack1");
    }

    // Attack2ボタンが押されたときに呼ばれる
    public void OnAttack2()
    {
        if (this.isJumping || this.isGuarding || !this.canMove || this.freezingTime > 0) return;

        this.animator.SetTrigger("Attack2");
        audioSource.PlayOneShot(clips[1]);
    }

    // ガードキーが押されているときに呼ぶ
    public void OnGuard() {
        if (this.isJumping || this.isGuarding || !this.canMove || this.freezingTime > 0) return;

        // バリアを子オブジェクトとして生成する
        this.barrier = Instantiate(barrierPrefab);
        this.barrier.transform.parent = this.transform;
        this.barrier.transform.localPosition = new Vector3(0, 0.75f, 0);

        this.isGuarding = true;
    }

    // ガードキーが離されたときに呼ぶ
    public void OffGuard() {
        // バリアを消す
        if (this.barrier != null) Destroy(this.barrier);

        this.isGuarding = false;
    }

    // 攻撃を受けたときに呼ばれる。ダメージ量と硬直時間を引数に受け取る
    public virtual void OnDamage(int damage, float freezingTime) {
        if (this.hp <= 0) return; // 死んでいたらダメージを受けない

        this.animator.SetTrigger("IsHurt");

        // ダメージを受ける
        if (!isGuarding) {
            this.hp -= damage;

            // ダメージ音再生
            this.audioSource.PlayOneShot(clips[0]);
        }
        else {
            this.hp -= (int)(damage * guardingRatio);

            this.audioSource.PlayOneShot(clips[2]);
        }

        this.slider.value = this.hp;

        // HPが0以下になったら死亡する
        if (this.hp <= 0) {
            Die();
            return;
        }

        // ガードしていないときは硬直を設定する
        if (!isGuarding) {
            this.freezingTime = freezingTime;
            this.spriteRenderer.color = new Color(255.0f, 0.0f, 0.0f);
        }
    }

    // やられたときの処理
    private void Die()
    {
        this.hp = 0;

        // 死亡アニメーションに移行
        this.animator.SetTrigger("IsDead");
        
        // 操作不能にする
        this.enabled = false;
        
        this.battle.Finish(playerNum);
    }

    //プレイヤー1or2を識別
    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    public int GetHp(){ return maxHp; }
    public float GetSpeed(){ return moveSpeed; }
    public float GetJump(){ return jumpForce; }
    
    public float GetGuardingRatio() { return this.guardingRatio; }
}
