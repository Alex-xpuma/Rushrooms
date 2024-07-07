using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public string url; // URL ссылки

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url); // Открываем URL в браузере
    }
}
