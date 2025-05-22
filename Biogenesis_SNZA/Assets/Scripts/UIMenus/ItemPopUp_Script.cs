using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUp_Script : MonoBehaviour
{
    public static ItemPopUp_Script Instance { get; private set; }

    public GameObject popupPrefab;
    public float popupDuration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemImage")?.GetComponent<Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        StartCoroutine(FadeOut(newPopup));
    }
    private IEnumerator FadeOut(GameObject popup)
    {
        //funcio per fer fadeout del gameobject popup lentament
        yield return new WaitForSeconds(popupDuration);
        if (popup == null) yield break;
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for (float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }
        Destroy(popup);
    }
}
