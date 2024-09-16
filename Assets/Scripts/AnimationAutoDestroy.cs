using UnityEngine;
using System.Collections;


/// <summary>
/// Clase que utilizamos para que las explosiones desaparezcan tras acabar su animación
/// </summary>
public class AnimationAutoDestroy : MonoBehaviour
{
    /// <summary>
    /// Número de veces que la animación se repite antes de eliminarse el gameObject
    /// </summary>
    public float repetitions = 3f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * repetitions);
    }
}