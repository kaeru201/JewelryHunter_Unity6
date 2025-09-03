using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの能力値")]
    public float speed = 5f;//プレイヤーのスピードを調整
    public float jumpPower = 9.0f;//ジャンプ力

    [Header("地面判定の対象レイヤー")]
    public LayerMask groundLayer;

    Rigidbody2D rbody; //PlayerについているRigidbodyを扱うための変数
    float axisH; //入力の方向を記憶するための変数
    bool goJump = false;//ジャンプフラグ (ture:真on false:偽off)
    bool onGround = false;//地面にいるかの判定



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();//Playerについているコンポネント情報を取得

    }

    // Update is called once per frame
    void Update()
    {
        //地面判定をサークルキャストで行って、その結果を変数onGroundに代入
        onGround = Physics2D.CircleCast(
            transform.position, //発射位置=プレイヤーの位置
            0.2f,
            new Vector2(0, 1.0f),
            0,
            groundLayer
            );


        //Velocityの元となる値の取得(右なら1.0f、左なら-1.0f、何もなければ0)
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0)
        {
            //右を向く
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            //左を向く
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //GetButtonDownメソッド→引数に指定したボタンが押されたらtureを返す、押されていなかったたらfalseを返す
        if (Input.GetButtonDown("Jump"))
        {
            Jump();//jumpメソッドの発動
        }


    }
    //1秒間に50回(50fps)繰り返すように制御しながら行う繰り返しメソッド
    private void FixedUpdate()
    {
        //Velocityに値を代入
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        if (goJump)
        {
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false;
        }
    }

    void Jump()
    {
        if (onGround)
        {
            goJump = true;//ジャンプフラグをon
        }

    }
}
