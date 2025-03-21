using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public enum snzaAttackType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaAttackType currentAttackType = snzaAttackType.CANGREJO;
    public bool isAttacking = false;
    public enum snzaParryType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public snzaParryType currentParryType = snzaParryType.CANGREJO;
    public bool isParrying = false;
    public enum snzaUltiType { NONE, MANTIS }
    [SerializeField] public snzaUltiType currentUltiType;
    public bool isDoingUlti = false;

    //variable que determinarà quin mal fa Liora amb aquell attack
    public float damageAttackLiora;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackType = snzaAttackType.CANGREJO;
    }
    // Update is called once per frame
    void Update()
    {
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
        if (Liora_Movement_Script.isGrabbingLedge) { return; }
        if (context.started)
        {
            switch (currentAttackType)
            {
                case snzaAttackType.CANGREJO:
                    damageAttackLiora = 30f;
                    //animacion en cuestion
                    break;

                case snzaAttackType.ESCARABAJO:
                    damageAttackLiora = 28f;
                    //animacion en cuestion
                    break;
            }
            isAttacking = true;
        }
    }
}