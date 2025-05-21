using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamageLiora_Script : MonoBehaviour
{



    public static DamageLiora_Script instance;
    public static bool isParrying = false;
    public static Collider2D collider;

    //logica pociones
    public GameObject pocionesPanel;
    public GameObject pocionPrefab;
    public static int pocionesCount = 3;
    public GameObject[] pociones;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        collider = GetComponent<Collider2D>();
        pociones = new GameObject[pocionesCount];
        for (int i = 0; i < pocionesCount; i++)
        {
            GameObject pocion = Instantiate(pocionPrefab, pocionesPanel.transform);
            pocion.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            pociones[i] = pocion; //guardar las pociones en la array de gameObjects
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RecibirDamage(transform.position, 50);
        }
    }
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UsePotion();
        }
    }
    public void UsePotion()
    {
        for (int i = pociones.Length - 1; i >= 0; i--)
        {
            Pociones_Script pocionesScript = pociones[i].GetComponent<Pociones_Script>();
            if (pocionesScript.pocionActiva)
            {
                GameControl_Script.lifeLiora += 200f;
                pocionesScript.PotionUsed();
                break;
            }
        }
    }
    public void RefillAllPotions()
    {
        foreach (GameObject potion in pociones)
        {
            potion.GetComponent<Pociones_Script>().PotionRecovered();
        }
    }
    public static void RecibirDamage(Vector2 enemy, float damage)
    {
        if (isParrying)
        {
            print("he hecho parry suuui");
            return;
        }
        else if (!isParrying)
        {
            //Knockback effect
            knockbackScript kb = collider.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(enemy, damage);
            }
            GameControl_Script.lifeLiora -= damage;
        }
        if (GameControl_Script.lifeLiora <= 0)
        {
            Liora_StateMachine_Script.isDying = true;
            instance.Invoke("LoadWhenDead", 2.5f);
        }
    }
    
    public void LoadWhenDead()
    {
        GameObject.FindObjectOfType<SaveController_Script>().LoadGame();
        Liora_StateMachine_Script.isDying = false;
    }
}
