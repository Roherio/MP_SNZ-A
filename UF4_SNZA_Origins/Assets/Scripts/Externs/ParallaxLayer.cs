using UnityEngine;


//Script que es fica a las layers que volem que es moguin, tambe podem dir quin es el valor que volem: valor + gran == m�s lluny, valor + petit == m�s aprop
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * (parallaxFactor/10);

        transform.localPosition = newPos;
    }

}