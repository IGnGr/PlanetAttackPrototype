using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que se utiliza para que el fondo sea dinámico, tanto el planeta rotando como el background en movimiento
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    /// <summary>
    /// Imagen que se usa como background
    /// </summary>
    [SerializeField] private RawImage backgroundImage;
    /// <summary>
    /// Cantidad de traslación en cada eje
    /// </summary>
    [SerializeField] private float x, y;
    /// <summary>
    /// Referencia al planeta en la escena
    /// </summary>
    [SerializeField] private GameObject planetGameObject;
    /// <summary>
    /// Ratio de rotación del planeta
    /// </summary>
    [SerializeField] private float planetRotationRate = 20f;

    void Update()
    {
        //Background Scroll
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImage.uvRect.size);

        //Planet rotation
        planetGameObject.transform.Rotate(0, 0, planetRotationRate*Time.deltaTime);
    }
}
