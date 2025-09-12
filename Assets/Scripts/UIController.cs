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

        buttonPanel.SetActive(false);//存在を非表示

        //時間さ
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

            //ステージクリアによってステージスコアが確定したのでトータルスコアに加算
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0;//次に備えてステージスコアはリセット

            timeCnt.isTimeOver = true;

            float times = timeCnt.displayTime;
            if (timeCnt.isCountDown)
            {
                //残時間をそのままタイムボーナスをトータルスコアに加算
                GameManager.totalScore += (int)times * 10;
            }

        else //カウントアップ
            {
                float gameTime = timeCnt.gameTime;
                GameManager.totalScore += (int)(gameTime - times) * 10;
            }

            UpdateScore();//UIに最終的な数字を反映

            audio.Stop();
            audio.PlayOneShot(soundController.bgm_GameClear);

            //2重にスコアを加算しないようgameclearのフラグは早々に変化（endには何も書いてないから）
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
                    //プレイヤーを見つけてきてそのPlayerControllerコンポーネントのGameOverメソッドをやらせている
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

            //スコアもリアルタイムに更新
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

