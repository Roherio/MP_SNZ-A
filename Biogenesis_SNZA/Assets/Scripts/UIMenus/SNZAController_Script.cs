using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNZAController_Script : MonoBehaviour
{
    public GameObject snzaPanel;

    public GameObject[] snzaChiquita;
    //////logica item en menú
    public GameObject[] snzaAmpliada;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < snzaAmpliada.Length; i++)
        {
            snzaAmpliada[i].SetActive(false);
        }
        /*for (int i = 0; i < snzaChiquita.Length; i++)
        {
            GameObject snza = Instantiate(snzaChiquita[i], snzaPanel.transform);
            snza.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }*/
    }
    public void AmpliarSNZA(int snzaNumber)
    {
        for (int i = 0; i < snzaAmpliada.Length; i++)
        {
            snzaAmpliada[i].SetActive(false);
        }
        snzaAmpliada[snzaNumber].SetActive(true);
    }
}
