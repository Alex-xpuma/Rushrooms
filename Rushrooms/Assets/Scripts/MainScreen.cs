using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI mushroomCoinText;
    [SerializeField] TextMeshProUGUI buterflyCoinText;


    private void Awake()
    {
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

    public void Runner()
    {
        SceneManager.LoadScene("Game");
    }

    public void Game2048()
    {
        SceneManager.LoadScene("2048");
    }
}
