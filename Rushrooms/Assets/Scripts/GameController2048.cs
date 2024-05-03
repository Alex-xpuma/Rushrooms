using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController2048 : MonoBehaviour
{
    public static GameController2048 instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] Cell2048[] allCells;

    public static Action<string> slide;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            slide("d");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            slide("a");
        }
    }

    public void SpawnFill()
    {

        bool isFull = true;
        for(int i = 0; i<allCells.Length; i++)
        {
            if (allCells[i].fill == null) 
            {
                isFull = false;
            }
        }
        if(isFull)
        {
            return;
        }

        int WhichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[WhichSpawn].transform.childCount!=0)
        {
            Debug.Log(allCells[WhichSpawn].name + "is already fiiled");
            SpawnFill();
            return;
        }
        float chance = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(chance);
        if(chance < .2f)
        {
            return;
        }
        else if(chance < .8f)
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);
            Debug.Log(2);
            fill2048 tempFillComp = tempFill.GetComponent<fill2048>();
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else {
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);
            Debug.Log(4);
            fill2048 tempFillComp = tempFill.GetComponent<fill2048>();
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }

    public void StartSpawnFill()
    {
        int WhichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[WhichSpawn].transform.childCount != 0)
        {
            Debug.Log(allCells[WhichSpawn].name + "is already fiiled");
            SpawnFill();
            return;
        }
       
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);
            Debug.Log(2);
            fill2048 tempFillComp = tempFill.GetComponent<fill2048>();
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);

    }

    public void GameOverCheck()
    {
        isGameOver++;
        if (isGameOver >= 9)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        
    }
}
