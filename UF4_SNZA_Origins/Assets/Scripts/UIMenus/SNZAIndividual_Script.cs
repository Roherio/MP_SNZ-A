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
    public Image snzaSprite;
    /*
    public static bool desbloqueandoJabali = false;
    public static bool desbloqueandoSecretario = false;*/
    /*public static bool jabaliDesbloqueado;
    public static bool secretarioDesbloqueado;*/
    private void Awake()
    {
        hoverPanel.SetActive(false);
        UnlockItem();
        /*LockItem();
        if (SNZAName == "Cangrejo")
        {
            UnlockItem();
        }*/
    }
    private void Start()
    {
        snzaSprite = GetComponent<Image>();
    }

    //----------------------------------------TODO ESTO ECHADO PARA ATRAS ENTREGA JUEVES


    /*
     // Start is called before the first frame update
     void Start()
     {
         if (SNZAName == "Jabali" && SNZAProgressControl_Script.snzaJabaliConseguida)
         {
             UnlockItem();
         }
         if (SNZAName == "Secretario" && SNZAProgressControl_Script.snzaJabaliConseguida)
         {
             UnlockItem();
         }
     }

     // Update is called once per frame
     void Update()
     {
         if (SNZAName == "Jabali" && SNZAProgressControl_Script.snzaJabaliConseguida)
         {
             print("desbloquea crack"); //----------------------------aqui no entra (PASO 4)
             UnlockItem();//--------------------------------------------NI AQUI TAMPOCO (PASO 5)
         }
         if (SNZAName == "Secretario" && SNZAProgressControl_Script.snzaSecretarioConseguida)
         {
             UnlockItem();
         }
     }*/
    public void OnClick()
    {
        Liora_SelectSNZA_Script.Instance.SelectSNZA(snzaSprite, SNZAName);
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
            print("activarmenu");
            hoverPanel.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverPanel != null)
        {
            print("desactivarMenu");
            hoverPanel.SetActive(false);
        }
    }
}
