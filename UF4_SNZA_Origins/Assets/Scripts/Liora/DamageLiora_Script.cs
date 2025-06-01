using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamageLiora_Script : MonoBehaviour
{
    //------------------TECLA MAGICA INVENCIBLE
    public static bool invencible = false;

    LioraAudioManager lioraAudioManager;
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
        lioraAudioManager = GameObject.FindGameObjectWithTag("LioraAudioManager").GetComponent<LioraAudioManager>();
        CameraFading fade = Camera.main.GetComponent<CameraFading>();
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
        //TECLA MAGICA funció que fa que cada cop que pulsis la tecla V et facis invencible o et desfacis invencible
        if (Input.GetKeyDown(KeyCode.V))
        {
            invencible = !invencible;
        }
    }
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(GameControl_Script.lifeLiora == GameControl_Script.maxLife) { return; }
            Liora_StateMachine_Script.isTakingPotion = true;
            Invoke("UsePotion", 1f);
            Invoke("StopAnimation", 1.25f);
            //UsePotion();
        }
    }
    public void UsePotion()
    {
        for (int i = pociones.Length - 1; i >= 0; i--)
        {
            Pociones_Script pocionesScript = pociones[i].GetComponent<Pociones_Script>();
            if (pocionesScript.pocionActiva)
            {
                GameControl_Script.lifeLiora += 50f;
                if (GameControl_Script.lifeLiora > GameControl_Script.maxLife)
                {
                    GameControl_Script.lifeLiora = GameControl_Script.maxLife;
                }
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
        if (invencible) { return; }
        //Knockback effect
        knockbackScript kb = collider.GetComponent<knockbackScript>();
        if (kb != null)
        {
            kb.ApplyKnockback(enemy, damage);
        }
        GameControl_Script.lifeLiora -= damage;
        print("L'atac ha fet hit per " + damage + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        if (GameControl_Script.lifeLiora <= 0)
        {
            CameraFading fade = Camera.main.GetComponent<CameraFading>();
            fade.FadeOutSlow();
            Liora_StateMachine_Script.isDying = true;
            instance.Invoke("LoadWhenDead", 3.5f);
            
        }
    }
    public void StopAnimation()
    {
        Liora_StateMachine_Script.isTakingPotion = false;
    }
    public void LoadWhenDead()
    {
        GameObject.FindObjectOfType<SaveController_Script>().LoadGame();
        RefillAllPotions();
        Liora_StateMachine_Script.isDying = false;
    }
}