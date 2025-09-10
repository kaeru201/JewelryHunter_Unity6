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

    
     
    void Start()
    {
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false);//ë∂ç›ÇîÒï\é¶

        //éûä‘Ç≥
        Invoke("InactiveImage",1.0f);
    }

    
    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true);
            mainImage.SetActive(true);
            mainImage.GetComponent<Image>().sprite = gameClearSprite;

            retryButton.GetComponent<Button>().interactable = false;
        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true);
            mainImage.SetActive(true);
            mainImage.GetComponent<Image>().sprite = gameOverSprite;

            nextButton.GetComponent<Button>().interactable = false;

        }

        else if (GameManager.gameState == "playing")
        {
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();

                
        }

        



       
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}

