using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Liora_Attack_Script;

public class Liora_SelectSNZA_Script : MonoBehaviour
{
    public void SelectAttackCrab()
    {
        Liora_Attack_Script.currentAttackType = snzaAttackType.CANGREJO;
    }
    public void SelectParryCrab()
    {
        Liora_Attack_Script.currentParryType = snzaParryType.CANGREJO;
    }
    public void SelectAttackBoar()
    {
        Liora_Attack_Script.currentAttackType = snzaAttackType.JABALI;
    }
    public void SelectParryBoar()
    {
        Liora_Attack_Script.currentParryType = snzaParryType.JABALI;
    }
}