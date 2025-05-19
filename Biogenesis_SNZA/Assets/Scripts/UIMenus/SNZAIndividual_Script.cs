using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SNZAIndividual_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string SNZAName;
    public CanvasGroup canvasGroup;
    public GameObject hoverPanel; //panel que mostrara cuando estes por encima de el como boton
    public bool isUnlocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hoverPanel.SetActive(false);
        LockItem();
        if (SNZAName == "Cangrejo")
        {
            UnlockItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnlockItem()
    {
        isUnlocked = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        //canvasGroup.blocksRaycasts = true;
    }
    public void LockItem()
    {
        isUnlocked = false;
        canvasGroup.alpha = 0.4f;
        canvasGroup.interactable = false;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(false);
        }
    }
}
