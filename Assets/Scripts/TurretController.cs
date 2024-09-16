using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que gestiona la rotaci�n de las torretas
/// </summary>
public class TurretController : MonoBehaviour
{
    /// <summary>
    /// Posici�n del puntero del jugador
    /// </summary>
    Vector2 pointerPosition;


    // Update is called once per frame
    void Update()
    {
        PointTowardsMouse();
    }

    /// <summary>
    /// Mantiene las torretas mirando hacia la posici�n del cursor del jugador
    /// </summary>
    void PointTowardsMouse()
    {
        //Transformamos la posici�n del rat�n al sistema de coordenadas de la escena
        pointerPosition = Camera.main.ScreenToWorldPoint(CustomPointer.pointerPosition);

        //Obtenemos el vector direcci�n hacia el puntero
        Vector2 turretPosition = this.transform.position;
        Vector2 vectorToPointer = (pointerPosition - turretPosition).normalized;

        //Fijamos el vector up para rotar en la direcci�n hacia el puntero
        this.transform.up = vectorToPointer;

        Vector3 clampedRotation = this.transform.rotation.eulerAngles;

        //Clampeamos para que no se puedan rotar m�s de 45 grados hacia ninguno de los lados y crear puntos ciegos.
        if (clampedRotation.z > 90) clampedRotation.z = Mathf.Clamp(clampedRotation.z - 360, -45.0f, 0.0f) + 360;
        else clampedRotation.z = Mathf.Clamp(clampedRotation.z, 0.0f, 45.0f);

        //Fijamos la rotaci�n.
        transform.rotation = Quaternion.Euler(clampedRotation);
    }
}
