using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController2048 : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;
    [SerializeField] Transform[] allCells;

    public static Action<string> slide;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            slide("d");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            slide("a");
        }
    }

    public void SpawnFill()
    {
        int WhichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[WhichSpawn].childCount!=0)
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
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn]);
            Debug.Log(2);
            fill2048 tempFillComp = tempFill.GetComponent<fill2048>();
            allCells[WhichSpawn].GetComponent<fill2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else {
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn]);
            Debug.Log(4);
            fill2048 tempFillComp = tempFill.GetComponent<fill2048>();
            allCells[WhichSpawn].GetComponent<fill2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }
}
