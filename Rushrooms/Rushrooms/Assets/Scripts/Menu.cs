using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    GameObject optionsMenu;
    GameObject helpMenu;

    [SerializeField] TextMeshProUGUI mushroomCoinText;
    [SerializeField] TextMeshProUGUI buterflyCoinText;
    [SerializeField] GameObject blackPanel;

    bool isOptionsOpen;
    bool isHelpOpen;

    private void Awake()
    {
        

        optionsMenu = GameObject.Find("OptionsMenu");
        helpMenu = GameObject.Find("HelpMenu");

        optionsMenu.SetActive(false);
        isHelpOpen = false;
        isOptionsOpen = false;
        blackPanel.SetActive(false);
        helpMenu.SetActive(false);

        mushroomCoinText.text = Mathf.FloorToInt(GameData.CoinData.mushroomCoin).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OptionsMenu()
    {
        if(isHelpOpen)
            helpMenu.SetActive(false);
        optionsMenu.SetActive(true);
        blackPanel.SetActive(true);
        isOptionsOpen=true;
    }
    public void HelpMenu()
    {
        if (isOptionsOpen)
            optionsMenu.SetActive(false);
        helpMenu.SetActive(true);
        blackPanel.SetActive(true);
        isHelpOpen = true;
    }

    public void start()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void QuitOptionsMenu()
    {
        optionsMenu.SetActive(false);
        blackPanel.SetActive(false);
    }
    public void QuitHelpMenu()
    {
        helpMenu.SetActive(false);
        blackPanel.SetActive(false);
    }
}
