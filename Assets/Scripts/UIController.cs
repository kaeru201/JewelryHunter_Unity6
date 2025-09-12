using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public GameObject mainImage;
    public GameObject buttonPanel;

    public GameObject retryButton;
    public GameObject nextButton;

    public Sprite gameClearSprite;
    public Sprite gameOverSprite;

    TimeController timeCnt;
    public GameObject timeText;

    public GameObject scoreTxet;

    AudioSource audio;
    SoundController soundController;


    void Start()
    {
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false);//���݂��\��

        //���Ԃ�
        Invoke("InactiveImage", 1.0f);

        UpdateScore();

        audio = GetComponent<AudioSource>();
        soundController = GetComponent<SoundController>();

    }


    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true);
            mainImage.SetActive(true);
            mainImage.GetComponent<Image>().sprite = gameClearSprite;

            retryButton.GetComponent<Button>().interactable = false;

            //�X�e�[�W�N���A�ɂ���ăX�e�[�W�X�R�A���m�肵���̂Ńg�[�^���X�R�A�ɉ��Z
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0;//���ɔ����ăX�e�[�W�X�R�A�̓��Z�b�g

            timeCnt.isTimeOver = true;

            float times = timeCnt.displayTime;
            if (timeCnt.isCountDown)
            {
                //�c���Ԃ����̂܂܃^�C���{�[�i�X���g�[�^���X�R�A�ɉ��Z
                GameManager.totalScore += (int)times * 10;
            }

        else //�J�E���g�A�b�v
            {
                float gameTime = timeCnt.gameTime;
                GameManager.totalScore += (int)(gameTime - times) * 10;
            }

            UpdateScore();//UI�ɍŏI�I�Ȑ����𔽉f

            audio.Stop();
            audio.PlayOneShot(soundController.bgm_GameClear);

            //2�d�ɃX�R�A�����Z���Ȃ��悤gameclear�̃t���O�͑��X�ɕω��iend�ɂ͉��������ĂȂ�����j
            GameManager.gameState = "gameend";

        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true);
            mainImage.SetActive(true);
            mainImage.GetComponent<Image>().sprite = gameOverSprite;

            nextButton.GetComponent<Button>().interactable = false;

            timeCnt.isTimeOver = true ;

            audio.Stop();
            audio.PlayOneShot(soundController.bgm_GameOver);


            GameManager.gameState = "gameend";

        }

        else if (GameManager.gameState == "playing")
        {
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();

            if(timeCnt.isCountDown)
            {
                if(timeCnt.displayTime <= 0)
                {
                    //�v���C���[�������Ă��Ă���PlayerController�R���|�[�l���g��GameOver���\�b�h����点�Ă���
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                if(timeCnt.displayTime >= timeCnt.gameTime)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }

            //�X�R�A�����A���^�C���ɍX�V
            UpdateScore();


        }






    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }


    void UpdateScore()
    {
        int score = GameManager.stageScore + GameManager.totalScore;
        scoreTxet.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

}

