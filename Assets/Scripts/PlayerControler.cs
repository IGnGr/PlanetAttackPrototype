using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Clase que gestiona la l�gica referente a los disparos del jugador y movimiento de las torretas que los realizan.
/// </summary>
public class PlayerControler : MonoBehaviour
{
    /// <summary>
    /// Referencia al parent con todas las torretas
    /// </summary>
    public GameObject turretsParent;
    /// <summary>
    /// Indica si el jugador ha pulsado el bot�n de disparo
    /// </summary>
    private bool inputShoot;
    /// <summary>
    /// Prefab del proyectil a disparar
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// Posici�n del cursor donde apunta el jugador
    /// </summary>
    private Vector2 pointerPosition;
    /// <summary>
    /// Delay entre disparos
    /// </summary>
    private float consecutiveShotsDelay = 1f;
    /// <summary>
    /// El momento en el que se produjo el ultimo disparo.
    /// </summary>
    private float lastShotTime = 0f;
    /// <summary>
    /// Velocidad de los disparos del jugador
    /// </summary>
    private float playerShotSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        //Checkeamos si el jugador ha pulsado el bot�n de disparo
        inputShoot = Mouse.current.leftButton.wasReleasedThisFrame;


        //Si ha disparado, guardamos la posici�n en ese momento y determinamos que torreta deber� disparar hacia all�.
        //Disparar� la m�s cercana
        if (inputShoot)
        {
            pointerPosition = Camera.main.ScreenToWorldPoint(CustomPointer.pointerPosition);
            
            Transform closestTurret = DetermineClosestTurret(pointerPosition);
            Shoot(closestTurret);
        }
    }

    /// <summary>
    /// Funci�n para determinar la torreta m�s cercana al punto de disparo
    /// </summary>
    /// <param name="pointerPosition">Posici�n a la que disparar</param>
    /// <returns>Torreta m�s cercana al punto proporcionado, por lo que ser� la que disparar�</returns>
    private Transform DetermineClosestTurret(Vector2 pointerPosition)
    {
        Transform[] transforms = turretsParent.GetComponentsInChildren<Transform>();
        float minDistance = float.MaxValue;
        Transform closestToTarget = transforms[0];
        //Empieza en 1 al ser 0 el parent
        for ( int i = 1; i < transforms.Length; i++)
        {
            float currentElementDistance = Vector3.Distance(transforms[i].position, pointerPosition);
            if (minDistance > currentElementDistance)
            {
                closestToTarget = transforms[i];
                minDistance = currentElementDistance;
            }
        }
        return closestToTarget;
    }

    /// <summary>
    /// Funci�n para gestionar el disparo. Genera el proyectil y le proporciona los datos necesarios.
    /// </summary>
    /// <param name="fromTurret">La torreta que dispara</param>
    void Shoot(Transform fromTurret)
    {
        //Si ha pasado m�s tiempo que consecutiveShotsDelay, se genera el proyectil
        if( Time.timeSinceLevelLoad - lastShotTime >= consecutiveShotsDelay)
        {
            //Se instancia el prefab
            GameObject shot = Instantiate(bulletPrefab, fromTurret.position, fromTurret.rotation, transform);
            ProjectileController projectile = shot.GetComponent<ProjectileController>();
            //Se le proporciona el destino y la velocidad
            projectile.destination = pointerPosition;
            projectile.speed = playerShotSpeed;

            //Guardamos el momento en el que se ha disparado para gestionar el delay
            lastShotTime = Time.timeSinceLevelLoad;
        }
    }

}