using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioController_Script : MonoBehaviour
{
    public GameObject inventarioPanel;
    /*
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;*/

    //////logica item en menú
    public GameObject[] objeto;
    /*
    public Image[] imagenObjeto;
    public TextMeshPro[] tituloObjeto;
    public GameObject[] descripcionObjeto;*/

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objeto.Length; i++)
        {
            objeto[i].SetActive(false);
        }
        /*
        for (int i = 0; i < slotCount; i++)
        {
            Slot_Script slot = Instantiate(slotPrefab, inventarioPanel.transform).GetComponent<Slot_Script>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.itemInSlot = item;
            }
        }*/
    }
    public void ActivarItem(int itemNumber)
    {
        for (int i = 0; i < objeto.Length; i++)
        {
            objeto[i].SetActive(false);
        }
        objeto[itemNumber].SetActive(true);
    }
}
