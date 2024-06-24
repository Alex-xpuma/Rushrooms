using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    GameObject optionsMenu;
    GameObject helpMenu;

    private void Awake()
    {
        optionsMenu = GameObject.Find("OptionsMenu");
        helpMenu = GameObject.Find("HelpMenu");

        optionsMenu.SetActive(false);
        helpMenu.SetActive(false);
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
        optionsMenu.SetActive(true);
    }
    public void HelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void start()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void QuitOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }
    public void QuitHelpMenu()
    {
        helpMenu.SetActive(false);
    }
}
