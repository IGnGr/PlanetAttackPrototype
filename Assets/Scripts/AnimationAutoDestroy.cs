using UnityEngine;
using System.Collections;


/// <summary>
/// Clase que utilizamos para que las explosiones desaparezcan tras acabar su animaci�n
/// </summary>
public class AnimationAutoDestroy : MonoBehaviour
{
    /// <summary>
    /// N�mero de veces que la animaci�n se repite antes de eliminarse el gameObject
    /// </summary>
    public float repetitions = 3f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * repetitions);
    }
}