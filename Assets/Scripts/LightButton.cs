using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
