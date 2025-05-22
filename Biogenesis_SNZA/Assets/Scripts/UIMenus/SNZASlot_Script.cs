using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNZASlot_Script : MonoBehaviour
{
    public Image slotImage;
    private void Start()
    {
        slotImage = GetComponent<Image>();
    }
    public void OnClick()
    {
        Liora_SelectSNZA_Script.Instance.AsignarSNZAalSlot(slotImage);
    }
}
