using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public Animator animator;
    //---------------------------------------ATAQUE LOGIC
    [SerializeField] public enum snzaAttackType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaAttackType currentAttackType = snzaAttackType.CANGREJO;
    public static bool isAttacking = false;
    public float inputAttackCooldown = 0.2f;
    private float inputCooldownTimer;
    //combo Logic
    public static int currentComboStep = 0;
    private int maxComboSteps;
    private bool canReceiveNextComboInput = true;
    //private bool bufferedNextComboInput = false; //variable que permet pulsar el següent atac encara que no haguem acabat el que s'esta fent
    private float comboTimer = 0f;
    private float comboMaxTime = 1f;
    public bool isComboActive = false;
    //posició on s'instanciaran els attacks Liora
    public Transform attackLocation;
    //collider general
    private GameObject colliderAtaque;
    public GameObject colliderParry;
    //colliders particulars
    public GameObject colliderAttackCrabLiora;
    public GameObject colliderAttackBoarLiora;
    //---------------------------------------PARRY LOGIC
    public enum snzaParryType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaParryType currentParryType = snzaParryType.CANGREJO;
    public static bool isParrying = false;
    private float parryCooldown = 2f;
    //---------------------------------------ULTI LOGIC
    public enum snzaUltiType { NONE, MANTIS }
    [SerializeField] public snzaUltiType currentUltiType;
    public static bool isDoingUlti = false;

    //---------------------------------------VARIABLES PER CADA ATAC
    public static float damageAttackLiora; //variable que determinarà quin mal fa Liora amb aquell attack
    public float deactivateAction; //variable per saber quan acaba l'estat isAttacking/isParrying/isDoingUlti per cada moviment
    public float duracioCollider; //determina quant temps està el collider de l'atac instanciat
    public float delayCollider; //determina quin delay té per instanciar-se

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAttackType = snzaAttackType.CANGREJO;
        currentParryType = snzaParryType.JABALI;
        currentUltiType = snzaUltiType.NONE;
    }
    // Update is called once per frame
    void Update()
    {
        inputCooldownTimer += Time.deltaTime;
        parryCooldown -= Time.deltaTime;
        if (isComboActive)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboMaxTime)
            {
                ResetCombo();
            }
        }
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
    }
    //--------------------------------------------ATAQUE
    public void Ataque(InputAction.CallbackContext context)
    {
        //no entrarem a fer l'atac si el cooldownTimer segueix sent mes petit que el cooldown de l'atac
        if (GameControl_Script.isPaused) { return; }
        if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isParrying || isDoingUlti) { return; }
        if (context.started)
        {
            isParrying = false;
            if (!isComboActive)
            {
                currentComboStep = 1;
                isComboActive = true;
                comboTimer = 0f;
                HandleAttackStep(currentComboStep);
            }
            else if (canReceiveNextComboInput && comboTimer <= comboMaxTime && currentComboStep < 3)
            {
                currentComboStep++;
                comboTimer = 0f;
                canReceiveNextComboInput = false;
                HandleAttackStep(currentComboStep);
            }
        }
    }
    private void HandleAttackStep(int step)
    {
        switch (currentAttackType)
        {
            case snzaAttackType.CANGREJO:
                inputAttackCooldown = 0.2f;
                maxComboSteps = 3;
                switch (step)
                {
                    case 1:
                        damageAttackLiora = 20f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.1f;
                        deactivateAction = 0.3f;
                        break;
                    case 2:
                        damageAttackLiora = 30f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.1f;
                        deactivateAction = 0.7f;
                        break;
                    case 3:
                        damageAttackLiora = 50f;
                        duracioCollider = 0.3f;
                        delayCollider = 0.2f;
                        deactivateAction = 0.8f;
                        break;
                }
                colliderAtaque = colliderAttackCrabLiora;
                Invoke("CallInstanciarAtaque", delayCollider);
                //InstanciarAtaque(colliderAttackCrabLiora);
                break;
            case snzaAttackType.JABALI:
                inputAttackCooldown = 0.6f;
                maxComboSteps = 2;
                switch (step)
                {
                    case 1:
                        damageAttackLiora = 40f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.1f;
                        deactivateAction = 0.6f;
                        break;
                    case 2:
                        damageAttackLiora = 70f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.3f;
                        deactivateAction = 0.9f;
                        break;
                }
                colliderAtaque = colliderAttackBoarLiora;
                Invoke("CallInstanciarAtaque", delayCollider);
                break;
        }
        isAttacking = true;
        inputCooldownTimer = 0f;
        StartCoroutine(DeactivateAction());
    }
    void CallInstanciarAtaque()
    {
        //funcio intermitja a la que fem INVOKE donantli el collider que volem utilitzar
        InstanciarAtaque(colliderAtaque);
    }
    void InstanciarAtaque(GameObject collider)
    {
        Instantiate(collider, attackLocation);
        AttackCollider_Script.duracioCollider = duracioCollider;
    }
    //--------------------------------------------PARRY
    public void Parry(InputAction.CallbackContext context)
    {
        if (GameControl_Script.isPaused) { return; }
        //no entrarem a fer l'atac si el cooldownTimer segueix sent mes petit que el cooldown de l'atac
        if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isAttacking || isParrying || isDoingUlti) { return; }
        if (parryCooldown > 0f) { return; }
        if (context.started)
        {
            switch (currentParryType)
            {
                case snzaParryType.CANGREJO:
                    //aqui determinem el temps que trigarà despres en acabarse l'animació de parry, i també ressetejem el cooldownTimer perquè no pugui spammejar el parry
                    duracioCollider = 0.4f;
                    deactivateAction = 1f;
                    break;

                case snzaParryType.JABALI:
                    duracioCollider = 0.4f;
                    deactivateAction = 0.5f;
                    break;
            }
            InstanciarParry(colliderParry);
            inputCooldownTimer = 0f;
            isParrying = true;
            parryCooldown = 1.5f;
            StartCoroutine(DeactivateAction());
        }
    }
    void InstanciarParry(GameObject collider)
    {
        Instantiate(collider, attackLocation);
        ParryCollider_Script.duracioCollider = duracioCollider;
    }
    private IEnumerator DeactivateAction()
    {
        yield return new WaitForSeconds(deactivateAction);
        isParrying = false;
        isDoingUlti = false;
        canReceiveNextComboInput = true;
        //si aquest era el ultim hit del combo
        if (!isComboActive || currentComboStep >= maxComboSteps)
        {
            //isAttacking = false;
            ResetCombo();
        }
    }
    private void ResetCombo()
    {
        comboTimer = 0f;
        currentComboStep = 0;
        isComboActive = false;
        canReceiveNextComboInput = true;
        isAttacking = false;
    }
}