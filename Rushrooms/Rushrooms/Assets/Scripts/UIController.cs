using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Player player;
    [SerializeField] TextMeshProUGUI distanceText;

    GameObject results;
    [SerializeField] TextMeshProUGUI finaldistanceText;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        results = GameObject.Find("Results");
        results.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finaldistanceText.text = distance + " m";
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("Runner");
    }
}
