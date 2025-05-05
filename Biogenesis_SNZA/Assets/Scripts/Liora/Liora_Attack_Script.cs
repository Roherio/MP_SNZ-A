using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public Animator animator;
    public enum snzaAttackType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaAttackType currentAttackType = snzaAttackType.CANGREJO;
    public static bool isAttacking = false;
    public float inputAttackCooldown = 0.8f;
    private float inputCooldownTimer;

    //combo Logic
    public static int currentComboStep = 0;
    private bool canReceiveNextComboInput = true;
    //private bool bufferedNextComboInput = false; //variable que permet pulsar el següent atac encara que no haguem acabat el que s'esta fent
    private float comboTimer = 0f;
    private float comboMaxTime = 1f;
    public bool isComboActive = false;

    public enum snzaParryType { NONE, CANGREJO, ESCARABAJO, SECRETARIO, AGUILA, JABALI }
    [SerializeField] public static snzaParryType currentParryType = snzaParryType.CANGREJO;
    public static bool isParrying = false;
    private float parryCooldown = 2f;
   
    public enum snzaUltiType { NONE, MANTIS }
    [SerializeField] public snzaUltiType currentUltiType;
    public static bool isDoingUlti = false;

    //----Colliders attack Liora
    public Transform attackLocation;
    public float duracioCollider;
    public float delayCollider;
    //collider general
    private GameObject colliderAtaque;
    //colliders particulars
    public GameObject colliderAttackCrabLiora;
    public GameObject colliderAttackBoarLiora;

    //variable que determinarà quin mal fa Liora amb aquell attack
    public static float damageAttackLiora;
    //variable per saber quan acaba l'estat isAttacking/parrying/doingUlti per cada moviment
    public float deactivateAction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAttackType = snzaAttackType.CANGREJO;
        currentParryType = snzaParryType.CANGREJO;
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
                    //aqui determinem el temps que trigarà despres en acabarse l'animació d'attack, i també ressetejem el cooldownTimer perquè no pugui spammejar el atac
                    deactivateAction = 1f;
                    break;

                case snzaParryType.JABALI:
                    deactivateAction = 1f;
                    break;
            }
            inputCooldownTimer = 0f;
            isParrying = true;
            parryCooldown = 2f;
            StartCoroutine(DeactivateAction());
        }
    }
    private void HandleAttackStep(int step)
    {
        switch (currentAttackType)
        {
            case snzaAttackType.CANGREJO:
                switch (step)
                {
                    case 1:
                        damageAttackLiora = 20f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.2f;
                        deactivateAction = 0.6f;
                        break;
                    case 2:
                        damageAttackLiora = 30f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.2f;
                        deactivateAction = 0.5f;
                        break;
                    case 3:
                        damageAttackLiora = 50f;
                        duracioCollider = 0.3f;
                        delayCollider = 0.6f;
                        deactivateAction = 1.5f;
                        break;
                }
                colliderAtaque = colliderAttackCrabLiora;
                Invoke("CallInstanciarAtaque", delayCollider);
                //InstanciarAtaque(colliderAttackCrabLiora);
                break;

            case snzaAttackType.JABALI:
                damageAttackLiora = 50f;
                deactivateAction = 0.5f;
                break;
        }
        isAttacking = true;
        inputCooldownTimer = 0f;
        StartCoroutine(DeactivateAction());
    }
    void CallInstanciarAtaque()
    {
        InstanciarAtaque(colliderAtaque);
    }
    void InstanciarAtaque(GameObject collider)
    {
        Instantiate(collider, attackLocation);
        AttackCollider_Script.duracioCollider = duracioCollider;
    }
    private IEnumerator DeactivateAction()
    {
        /*if (currentComboStep == 4)
        {
            AnimationClip currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            print(currentClip.length);
            yield return new WaitForSeconds(currentClip.length);
        }
        else
        {
            yield return new WaitForSeconds(deactivateAction);
        }*/
        yield return new WaitForSeconds(deactivateAction);
        print(deactivateAction);
        isParrying = false;
        isDoingUlti = false;
        canReceiveNextComboInput = true;
        //si aquest era el ultim hit del combo
        if (!isComboActive || currentComboStep >= 3)
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