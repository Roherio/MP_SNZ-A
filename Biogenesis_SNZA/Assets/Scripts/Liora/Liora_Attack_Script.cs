using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public enum snzaAttackType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaAttackType currentAttackType = snzaAttackType.CANGREJO;
    public static bool isAttacking = false;
    public float inputAttackCooldown = 0.8f;
    private float inputCooldownTimer;
    
    public enum snzaParryType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaParryType currentParryType = snzaParryType.CANGREJO;
    public static bool isParrying = false;
   
    public enum snzaUltiType { NONE, MANTIS }
    [SerializeField] public snzaUltiType currentUltiType;
    public static bool isDoingUlti = false;

    //variable que determinarà quin mal fa Liora amb aquell attack
    public float damageAttackLiora;
    //variable per saber quan acaba l'estat isAttacking/parrying/doingUlti per cada moviment
    public float deactivateAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackType = snzaAttackType.CANGREJO;
        currentParryType = snzaParryType.CANGREJO;
    }
    // Update is called once per frame
    void Update()
    {
        inputCooldownTimer += Time.deltaTime;
        //comprovació de quin estat hem de passar a la StateMachine (prioritzem ulti, despres parry i després attack, per ressetejar les variables
        /*
        if (isDoingUlti)
        {
            isAttacking = false;
            isParrying = false;
        }
        else
        {
            if (isParrying)
            {
                isAttacking = false;
                isDoingUlti = false;
            }
            else if (isAttacking)
            {
                isParrying = false;
                isDoingUlti = false;
            }
        }
        */
        //pas de variables a la stateMachine
        Liora_StateMachine_Script.isAttacking = isAttacking;
        Liora_StateMachine_Script.isParrying = isParrying;
        Liora_StateMachine_Script.isDoingUlti = isDoingUlti;
        //NO HACERLO EN EL UPDATE, MIRAR DE HACERLO EN LA FUNCION DEL ATAQUE
        /*switch (scrMemoria.tipoSNZAAtaque)
            case "cangerjo"
            snzaAttackType.CANGREJO;*/
    }
    public void Ataque(InputAction.CallbackContext context)
    {
        //no entrarem a fer l'atac si el cooldownTimer segueix sent mes petit que el cooldown de l'atac
        if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer) { return; }
        if (context.started)
        {
            switch (currentAttackType)
            {
                case snzaAttackType.CANGREJO:
                    damageAttackLiora = 30f;
                    //aqui determinem el temps que trigarà despres en acabarse l'animació d'attack, i també ressetejem el cooldownTimer perquè no pugui spammejar el atac
                    deactivateAttack = 0.5f;
                    inputCooldownTimer = 0f;
                    break;

                case snzaAttackType.JABALI:
                    damageAttackLiora = 50f;
                    deactivateAttack = 0.5f;
                    inputCooldownTimer = 0f;
                    break;
            }
            isAttacking = true;
            StartCoroutine(DeactivateAttack());
        }
    }
    public void Parry(InputAction.CallbackContext context)
    {
        //no entrarem a fer l'atac si el cooldownTimer segueix sent mes petit que el cooldown de l'atac
        if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer) { return; }
        if (context.started)
        {
            switch (currentParryType)
            {
                case snzaParryType.CANGREJO:
                    //aqui determinem el temps que trigarà despres en acabarse l'animació d'attack, i també ressetejem el cooldownTimer perquè no pugui spammejar el atac
                    deactivateAttack = 1f;
                    inputCooldownTimer = 0f;
                    break;

                case snzaParryType.JABALI:
                    deactivateAttack = 1f;
                    inputCooldownTimer = 0f;
                    break;
            }
            isParrying = true;
            StartCoroutine(DeactivateAttack());
        }
    }
    private IEnumerator DeactivateAttack()
    {
        yield return new WaitForSeconds(deactivateAttack);
        isAttacking = false;
        isParrying = false;
        isDoingUlti = false;
    }
}