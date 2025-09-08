using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject mainImage;
    public GameObject buttonPanel;

    public GameObject retryButton;
    public GameObject nextButton;

    public Sprite gameClearSprite;
    public Sprite gameOverSprite;
    
    void Start()
    {
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
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}

