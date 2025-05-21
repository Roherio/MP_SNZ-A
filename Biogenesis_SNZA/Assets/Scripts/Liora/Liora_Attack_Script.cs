using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class Liora_Attack_Script : MonoBehaviour
{
    public Animator animator;
    LioraAudioManager audioManager;
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
    
    private float comboTimer = 0f;
    private float comboMaxTime = 0.7f;
    public bool isComboActive = false;
    //BUFFER DEL INPUT DE ATAQUE
    private enum InputType { ATTACK, PARRY}
    private Queue<InputType> inputBuffer = new Queue<InputType>();
    private float inputBufferTime = 0.2f;
    private float currentBufferTimer;
    //posició on s'instanciaran els attacks Liora
    public Transform attackLocation;
    //---------------------------------------VARIABLES PER CADA ATAC
    public static float damageAttackLiora; //variable que determinarà quin mal fa Liora amb aquell attack
    //public float deactivateAction; //variable per saber quan acaba l'estat isAttacking/isParrying/isDoingUlti per cada moviment
    public float duracioCollider; //determina quant temps està el collider de l'atac instanciat
    public float delayCollider; //determina quin delay té per instanciar-se

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
    /*public enum snzaUltiType { NONE, MANTIS }
    [SerializeField] public snzaUltiType currentUltiType;
    public static bool isDoingUlti = false;*/
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("LioraAudioManager").GetComponent<LioraAudioManager>();
        animator = GetComponent<Animator>();
        currentAttackType = snzaAttackType.CANGREJO;
        currentParryType = snzaParryType.JABALI;
        //currentUltiType = snzaUltiType.NONE;
    }
    void Update()
    {
        inputCooldownTimer += Time.deltaTime;
        parryCooldown -= Time.deltaTime;
        //input buffer
        if (inputBuffer.Count > 0)
        {
            currentBufferTimer -= Time.deltaTime;
            if(currentBufferTimer <= 0)
            {
                inputBuffer.Clear();
            }
        }

        if (isComboActive)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboMaxTime)
            {
                ResetCombo();
            }
        }

        if (!isParrying && inputBuffer.Count > 0)
        {
            ProcessBufferedInput();
        }
        //pas de variables a la stateMachine
        Liora_StateMachine_Script.isAttacking = isAttacking;
        Liora_StateMachine_Script.isParrying = isParrying;
        //Liora_StateMachine_Script.isDoingUlti = isDoingUlti;
    }
    //--------------------------------------------ATAQUE
    public void Ataque(InputAction.CallbackContext context)
    {
        //no entrarem a fer l'atac si el cooldownTimer segueix sent mes petit que el cooldown de l'atac
        if (GameControl_Script.isPaused) { return; }
        if (Liora_Movement_Script.jumping || Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isParrying) { return; }
        if (context.started)
        {
            inputBuffer.Enqueue(InputType.ATTACK);
            currentBufferTimer = inputBufferTime;
        }
        /*if (context.started)
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
        }*/
    }
    private void ProcessBufferedInput()
    {
        if (inputBuffer.Count == 0) { return; }
        InputType input = inputBuffer.Peek();
        switch (input)
        {
            case InputType.ATTACK:
                if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isParrying) { return; }
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

                inputBuffer.Dequeue();
                break;
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
                        audioManager.LioraSFX(audioManager.voiceShortSlash);
                        audioManager.LioraSFX(audioManager.shorSlash);
                        break;
                    case 2:
                        damageAttackLiora = 30f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.1f;
                        audioManager.LioraSFX(audioManager.voiceShortSlash);
                        audioManager.LioraSFX(audioManager.shorSlash);
                        break;
                    case 3:
                        damageAttackLiora = 50f;
                        duracioCollider = 0.3f;
                        delayCollider = 0.2f;
                        audioManager.LioraSFX(audioManager.voiceLongSlash);
                        audioManager.LioraSFX(audioManager.longSLash);
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
                        break;
                    case 2:
                        damageAttackLiora = 70f;
                        duracioCollider = 0.2f;
                        delayCollider = 0.3f;
                        break;
                }
                colliderAtaque = colliderAttackBoarLiora;
                Invoke("CallInstanciarAtaque", delayCollider);
                break;
        }
        isAttacking = true;
        inputCooldownTimer = 0f;
        //Invoke("DeactivateAction", deactivateAction);
        //StartCoroutine(DeactivateAction());
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
        if (Liora_Movement_Script.jumping || Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isAttacking || isParrying) { return; }
        if (parryCooldown > 0f) { return; }
        if (context.started)
        {
            switch (currentParryType)
            {
                case snzaParryType.CANGREJO:
                    //aqui determinem el temps que trigarà despres en acabarse l'animació de parry, i també ressetejem el cooldownTimer perquè no pugui spammejar el parry
                    duracioCollider = 0.4f;
                    break;

                case snzaParryType.JABALI:
                    duracioCollider = 0.4f;
                    break;
            }
            InstanciarParry(colliderParry);
            inputCooldownTimer = 0f;
            isParrying = true;
            parryCooldown = 1.5f;
            //Invoke("DeactivateAction", deactivateAction);
            //StartCoroutine(DeactivateAction());
        }
    }
    void InstanciarParry(GameObject collider)
    {
        Instantiate(collider, attackLocation);
        ParryCollider_Script.duracioCollider = duracioCollider;
    }
    public void OnActionAnimationEnd()
    {
        if (!isComboActive || currentComboStep >= maxComboSteps)
        {
            ResetCombo();
        }
        isAttacking = false;
        isParrying = false;
        canReceiveNextComboInput = true;
    }
    /*private void DeactivateAction()
    {
        isAttacking = false;
        isParrying = false;
        //isDoingUlti = false;
        canReceiveNextComboInput = true;
        //si aquest era el ultim hit del combo
        if (!isComboActive || currentComboStep >= maxComboSteps)
        {
            //isAttacking = false;
            ResetCombo();
        }
    }*/
    /*private IEnumerator DeactivateAction()
    {
        yield return new WaitForSeconds(deactivateAction);
        isAttacking = false;
        isParrying = false;
        //isDoingUlti = false;
        canReceiveNextComboInput = true;
        //si aquest era el ultim hit del combo
        if (!isComboActive || currentComboStep >= maxComboSteps)
        {
            //isAttacking = false;
            ResetCombo();
        }
    }*/
    private void ResetCombo()
    {
        comboTimer = 0f;
        currentComboStep = 0;
        isComboActive = false;
        canReceiveNextComboInput = true;
        //isAttacking = false;
    }
}