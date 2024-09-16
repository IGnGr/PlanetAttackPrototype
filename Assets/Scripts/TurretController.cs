using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que gestiona la rotación de las torretas
/// </summary>
public class TurretController : MonoBehaviour
{
    /// <summary>
    /// Posición del puntero del jugador
    /// </summary>
    Vector2 pointerPosition;


    // Update is called once per frame
    void Update()
    {
        PointTowardsMouse();
    }

    /// <summary>
    /// Mantiene las torretas mirando hacia la posición del cursor del jugador
    /// </summary>
    void PointTowardsMouse()
    {
        //Transformamos la posición del ratón al sistema de coordenadas de la escena
        pointerPosition = Camera.main.ScreenToWorldPoint(CustomPointer.pointerPosition);

        //Obtenemos el vector dirección hacia el puntero
        Vector2 turretPosition = this.transform.position;
        Vector2 vectorToPointer = (pointerPosition - turretPosition).normalized;

        //Fijamos el vector up para rotar en la dirección hacia el puntero
        this.transform.up = vectorToPointer;

        Vector3 clampedRotation = this.transform.rotation.eulerAngles;

        //Clampeamos para que no se puedan rotar más de 45 grados hacia ninguno de los lados y crear puntos ciegos.
        if (clampedRotation.z > 90) clampedRotation.z = Mathf.Clamp(clampedRotation.z - 360, -45.0f, 0.0f) + 360;
        else clampedRotation.z = Mathf.Clamp(clampedRotation.z, 0.0f, 45.0f);

        //Fijamos la rotación.
        transform.rotation = Quaternion.Euler(clampedRotation);
    }
}
