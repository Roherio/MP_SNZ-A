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

    //logica pociones. determinamos que tenemos 3 pociones en este script y se instancian gameObjects pocionPrefab en funcion de cuantos hemos determinado.
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
        //instanciament de pocionsPrefab en funcio de quantes pocions hem determinat
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
    //funcio del INPUT SYSTEM per curar al personatge
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(GameControl_Script.lifeLiora == GameControl_Script.maxLife) { return; }
            //posem l'animacio del personatge fent la cura amb poció i invoquem els usos de la poció (i parar l'animacio)
            Liora_StateMachine_Script.isTakingPotion = true;
            Invoke("UsePotion", 1f);
            Invoke("StopAnimation", 1.25f);
        }
    }
    public void UsePotion()
    {
        //funció per utilitzar les pocions. Recorre la array de pocions que hem determinat (en aquest cas 3) i mira quines estan actives i quines no. Cada cop que s'utilitza una poció, aquesta dins seu guarda un valor bool que es diu pocionActiva, i el posa a false quan l'utilitzem. al recórrer l'array de pocions aquí, si ens trobem una poció inactiva seguirem recorrent el bucle for, i si ens trobem una poció activa utilitzarem la poció (canviantla a pocionActiva false), ens pujarem la vida i farem break d'aquesta funcio
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
        //funcio que recarrega totes les pocions (s'utilitza des de el Checkpoint_Script al descansar en un checkpoint
        foreach (GameObject potion in pociones)
        {
            potion.GetComponent<Pociones_Script>().PotionRecovered();
        }
    }
    public static void RecibirDamage(Vector2 enemy, float damage)
    {
        //si s'ha pulsat el botó invencible, return
        if (invencible) { return; }
        //Knockback effect
        knockbackScript kb = collider.GetComponent<knockbackScript>();
        if (kb != null)
        {
            kb.ApplyKnockback(enemy, damage);
        }
        GameControl_Script.lifeLiora -= damage;
        print("L'atac ha fet hit per " + damage + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        //si fos cas que ha rebut tant mal que la vida baixa de 0, passem a fer la animació de mort
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
    //funció per carregar partida quan morim
    public void LoadWhenDead()
    {
        GameObject.FindObjectOfType<SaveController_Script>().LoadGame();
        RefillAllPotions();
        Liora_StateMachine_Script.isDying = false;
    }
}