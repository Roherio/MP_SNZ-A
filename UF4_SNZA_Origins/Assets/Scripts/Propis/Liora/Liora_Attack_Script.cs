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
    public static bool isAttacking = false;
    public float inputAttackCooldown = 0.2f;
    private float inputCooldownTimer;
    //combo Logic
    public static int currentComboStep = 0;
    private int maxComboSteps;
    public static bool canReceiveNextComboInput = true;
    
    private float comboTimer = 0f;
    private float comboMaxTime = 0.7f;
    public bool isComboActive = false;

    //----------------------------------DISPARAR LOGIC
    public static bool isShooting = false;
    private float shootingTimer = 0f;
    public GameObject projectilPrefab;
    public static float damageProjectil = 20f;
    
    //------------------------------BUFFER DEL INPUT DE ATAQUE
    //el buffer ens permet que el jugador actui amb l'atac en el moment en el que el jugador s'alliberi. Per exemple, si estem atacant i fent la acció atac 1, no podem dirli que faci la accio d'atac 2 per seguir el combo. tot i així, amb el buffer, acumularem aquest input de l'atac 2 perquè quan el jugador acabi l'atac 1 i s'alliberi, s'instancïi l'atac 2 seguidament i sigui més comode pel jugador i no hagi de donar l'input perfecte just al acabar l'atac 1
    private enum InputType { ATTACK, PARRY}
    //utilitzem una queue per acumular inputs del jugador dins una cua de inputs, la queue es buidarà passat un temps curt, però mentre hi hagi elements en cua aquests es realitzaran seguidament quan el jugador pugui (quan acabi el salt, quan acabi l'accio que està fent ara mateix...
    private Queue<InputType> inputBuffer = new Queue<InputType>();
    private float inputBufferTime = 0.2f;
    private float currentBufferTimer;
    //posició on s'instanciaran els attacks Liora
    public Transform attackLocation;
    //---------------------------------------VARIABLES PER CADA ATAC del combo del personatge
    public static float damageAttackLiora; //variable que determinarà quin mal fa Liora amb aquell attack
    public float duracioCollider; //determina quant temps està el collider de l'atac instanciat
    public float delayCollider; //determina quin delay té per instanciar-se el collider que fa mal a l'enemic

    //collider que instanciarem en el moment que volguem atacar amb el nostre personatge a un enemic
    public GameObject colliderAtaque;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("LioraAudioManager").GetComponent<LioraAudioManager>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        inputCooldownTimer += Time.deltaTime;
        shootingTimer += Time.deltaTime;
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

        if (!isShooting && inputBuffer.Count > 0)
        {
            ProcessBufferedInput();
        }
        //pas de variables a la stateMachine
        Liora_StateMachine_Script.isAttacking = isAttacking;
    }
    
    
    //--------------------------------------------ATAQUE
    public void Ataque(InputAction.CallbackContext context)
    {
        //no entrarem a fer l'atac si el el controlador del joc sap que estem pausats
        if (GameControl_Script.isPaused) { return; }
        //tampoc entrarem si el cooldownTimer segueix sent mes petit que el cooldown de l'atac o estem fent alguna de les accions que no haurien de permetre'ns atacar (escalant, saltant, agafant-nos a una cantonada...)
        if (Liora_Movement_Script.jumping || Liora_Movement_Script.isGrabbingLedge || Liora_Movement_Script.isClimbing || inputAttackCooldown > inputCooldownTimer || isShooting) { return; }
        if (context.started)
        {
            inputBuffer.Enqueue(InputType.ATTACK);
            currentBufferTimer = inputBufferTime;
        }
    }
    
    
    //aquesta funció és la que s'encarrega de processar el buffer d'atacs (Buffer = acumulació de inputs d'atac perquè quan el jugador s'alliberi de l'anterior acció pugui instanciar la següent).
    private void ProcessBufferedInput()
    {
        //si els elements que es troben a la queue de inputBuffer són 0, no executarem cap accio
        if (inputBuffer.Count == 0) { return; }
        //amb la funció .Peek de Queue el que fem es fer una ullada a l'interior de la cua de inputs i veure què hi ha
        InputType input = inputBuffer.Peek();
        switch (input)
        {
            case InputType.ATTACK:
                if (Liora_Movement_Script.isGrabbingLedge || inputAttackCooldown > inputCooldownTimer || isShooting) { return; }
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
    
    //en aquesta funció és on configurem el valor de les variables damageAttack per les accions del personatge sobre els seus enemics. si és el pas 1 del combo, fa 20 de mal, si es el pas 2, fa 30 i si es el pas 3 (el mes fort) fa 50.
    private void HandleAttackStep(int step)
    {
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
        //necessitem ferli Invoke perquè el collider aparegui en un moment concret, quan quadra amb l'animació de l'atac, i que no es vegi raro que el personatge colpegi a l'enemic abans de que visualment ho faci
        Invoke("CallInstanciarAtaque", delayCollider);
        isAttacking = true;
        inputCooldownTimer = 0f;
    }
    void CallInstanciarAtaque()
    {
        //utilitzem aquesta funció intermitja per poder fer-li Invoke en el moment que volem. En l'interior d'aquesta és on li donem el collider que volem utilitzar
        InstanciarAtaque(colliderAtaque);
    }
    void InstanciarAtaque(GameObject collider)
    {
        Instantiate(collider, attackLocation);
        AttackCollider_Script.duracioCollider = duracioCollider;
    }
    //aquesta funció OnActionAnimationEnd ens serveix per col·locarla al final de cada arxiu .anim on guardem la animació de l'atac. Al acabar AttackCangrejo1, per exemple, tenim una tag que executa aquesta funció i fa que el jugador deixi d'estar en estat ATACANT, per tant passant a Idle.
    public void Disparar(InputAction.CallbackContext context)
    {
        //no entrarem a disparar si resulta que la nostra munició és 0 o menor
        if (GameControl_Script.municion <= 0) { return; }
        //no entrarem a instanciar el dispar de gel si el el controlador del joc sap que estem pausats
        if (GameControl_Script.isPaused) { return; }
        //tampoc entrarem si el timer de disparar és menor a 2 segons o estem fent alguna de les accions que no haurien de permetre'ns atacar (escalant, saltant, agafant-nos a una cantonada...)
        if (Liora_Movement_Script.jumping || Liora_Movement_Script.isGrabbingLedge || Liora_Movement_Script.isClimbing || shootingTimer < 2f || isAttacking) { return; }
        if (context.performed)
        {
            //resset del timer de disparar
            shootingTimer = 0f;
            GameObject projectil = Instantiate(projectilPrefab, attackLocation.position, Quaternion.identity);
            //en el moment d'instanciar el projectil, mirarem quin valor te isFacingRight de la stateMachine per saber cap a on mira liora (true = dreta, false = esquerra). llavors, passarem aquesta direcció una única vegada al prefab del projectil per tal de no fer-ho en un update com a la stateMachine, cosa que faria que el projectil anés canviant de direcció a l'aire cada cop que el personatge gira.
            projectil.GetComponent<Liora_Projectil_Script>().SetDirection(Liora_StateMachine_Script.isFacingRight);
        }
    }
    public void OnActionAnimationEnd()
    {
        if (!isComboActive || currentComboStep >= maxComboSteps)
        {
            ResetCombo();
        }
        isAttacking = false;
        isShooting = false;
        canReceiveNextComboInput = true;
    }
    private void ResetCombo()
    {
        comboTimer = 0f;
        currentComboStep = 0;
        isComboActive = false;
        canReceiveNextComboInput = true;
        //isAttacking = false;
    }
}