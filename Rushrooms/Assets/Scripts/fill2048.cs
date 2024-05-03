using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class fill2048 : MonoBehaviour
{
    public int value;
    [SerializeField] TextMeshProUGUI valueDisplay;
  
    public void FillValueUpdate(int valueIn)
    {
        value = valueIn;
        valueDisplay.text = value.ToString();
    }
}
