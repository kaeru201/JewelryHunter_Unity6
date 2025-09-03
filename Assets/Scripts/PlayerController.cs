using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̔\�͒l")]
    public float speed = 5f;//�v���C���[�̃X�s�[�h�𒲐�
    public float jumpPower = 9.0f;//�W�����v��

    [Header("�n�ʔ���̑Ώۃ��C���[")]
    public LayerMask groundLayer;

    Rigidbody2D rbody; //Player�ɂ��Ă���Rigidbody���������߂̕ϐ�
    float axisH; //���͂̕������L�����邽�߂̕ϐ�
    bool goJump = false;//�W�����v�t���O (ture:�^on false:�Uoff)
    bool onGround = false;//�n�ʂɂ��邩�̔���



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();//Player�ɂ��Ă���R���|�l���g�����擾

    }

    // Update is called once per frame
    void Update()
    {
        //�n�ʔ�����T�[�N���L���X�g�ōs���āA���̌��ʂ�ϐ�onGround�ɑ��
        onGround = Physics2D.CircleCast(
            transform.position, //���ˈʒu=�v���C���[�̈ʒu
            0.2f,
            new Vector2(0, 1.0f),
            0,
            groundLayer
            );


        //Velocity�̌��ƂȂ�l�̎擾(�E�Ȃ�1.0f�A���Ȃ�-1.0f�A�����Ȃ����0)
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0)
        {
            //�E������
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            //��������
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //GetButtonDown���\�b�h�������Ɏw�肵���{�^���������ꂽ��ture��Ԃ��A������Ă��Ȃ���������false��Ԃ�
        if (Input.GetButtonDown("Jump"))
        {
            Jump();//jump���\�b�h�̔���
        }


    }
    //1�b�Ԃ�50��(50fps)�J��Ԃ��悤�ɐ��䂵�Ȃ���s���J��Ԃ����\�b�h
    private void FixedUpdate()
    {
        //Velocity�ɒl����
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
            goJump = true;//�W�����v�t���O��on
        }

    }
}
