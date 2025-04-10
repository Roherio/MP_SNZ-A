using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1_Script : MonoBehaviour
{
    //els dos sliders que mostren la vida a la UI
    [SerializeField] private RectTransform redRect;
    [SerializeField] private RectTransform orangeRect;

    [SerializeField] private RectMask2D maskRed;
    [SerializeField] private RectMask2D maskOrange;

    private float maxMask;
    private float initialMask;

    //[SerializeField] private float delayStart = 0.3f;
    //[SerializeField] private float delaySpeed = 0.01f;

    void Start()
    {
        maxMask = redRect.rect.width - maskRed.padding.x - maskRed.padding.z;
        initialMask = maskRed.padding.z;
    }
    public void SetValue(int newValue)
    {
        //funcio que configura quant es croppeja la mascara per la dreta
        var targetWidth = newValue * maxMask / GameControl_Script.maxLife;
        var newRightMask = maxMask + initialMask - targetWidth;
        var padding = maskRed.padding;
        padding.z = newRightMask;
        maskRed.padding = padding;
    }
    void Update()
    {
        /*if (maskRed.padding != maskOrange.padding)
        {
            maskOrange.padding = Mathf.Lerp(maskOrange.padding, GameControl_Script.lifeLiora, delaySpeed);
        }*/
    }
    void TakeDamage(float damage)
    {
        GameControl_Script.lifeLiora -= damage;
    }

    /*IEnumerator DelayHealth()
    {
        yield return new WaitForSeconds(delayStart);
    }*/
}