using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public enum snzaAttackType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI}
    public enum snzaParryType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    public enum snzaUltiType { NONE, MANTIS}
    public float damageAttackLiora;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //NO HACERLO EN EL UPDATE, MIRAR DE HACERLO EN LA FUNCION DEL ATAQUE
        /*switch (scrMemoria.tipoSNZAAtaque)
            case "cangerjo"
            snzaAttackType.CANGREJO;*/
    }
    public void Ataque(InputAction.CallbackContext context)
    {
        //if (isGrabbingLedge) { return; }
        if (context.started)
        {
            switch snzaAttackType
            {
                case snzaAttackType.CANGREJO:
                    damageAttackLiora = 30f;
                    Animator.Play("animacionCangrejo");
                    break;

                case snzaAttackType.ESCARABAJO:
                    damageAttackLiora = 28f;
                    Animator.Play("animacionEscarabajo");
                    break;
            }
        }
    }
    /*private void Attack()
    {
            switch snzaAttackType
            {
                case snzaAttackType.CANGREJO:
                    damageAttackLiora = 30f;
                break;

                case snzaAttackType.ESCARABAJO:
                    damageAttackLiora = 28f;
                break;
            }
    }*/
}
