using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

/// <summary>
/// Clase que controla el ratio de aparición de los misiles enemigos
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Referencia al parent que contiene los distintos objetivos del enemigo aka nuestras vidas
    /// </summary>
    public GameObject objectivesParent;
    /// <summary>
    /// Referncia al parent que contiene los distintos puntos de Respawn posibles de los enemigos
    /// </summary>
    public GameObject spawnsParent;
    /// <summary>
    /// Prefab del proyectil a generar
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// Ratio de generación de misiles enemigos, contra menor es el número, mayor será el ratio de aparición.
    /// </summary>
    private int spawnRate= 500;

    private int maxSpawnRate = 15;
    private int minSpawnRate = 5;


    
    /// <summary>
    /// Cada cuanto tiempo se intenta generar un enemigo
    /// </summary>
    public float GenerationRateTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateEnemyShot());
        StartCoroutine(UpdateSpawnRate());

    }


    /// <summary>
    /// Corutina para ir actualizando el spawnRate enemigo y mejorar el gameplay
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateSpawnRate()
    {

        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

        yield return new WaitForSeconds(GenerationRateTime);

        //Se llama a sí misma para seguir actualizandose
        StartCoroutine(UpdateSpawnRate());
    }

    /// <summary>
    /// Corutina para generar un nuevo disparo
    /// </summary>
    IEnumerator GenerateEnemyShot()
    {
        //1 de cada spawnRate veces, se generará correctamente
        float rand = Random.Range(0, spawnRate + 1) % spawnRate;

        if( rand == 0 )
        {

            Vector3 origin = DetermineOrigin();
            Vector3 destination = DetermineObjective();

            //Obtenemos el vector hacia el objetivo
            Vector3 towardsDestination = destination - origin;

            //Instanciamos el prefab del disparo
            GameObject enemyShot = Instantiate(bulletPrefab, origin, Quaternion.identity, GameObject.Find("Match").transform);

            //Le fijamos el vector up para que esté con la rotación correcta 
            enemyShot.transform.up = towardsDestination;

            //Pasamos al objeto que creamos la información de su punto objetivo, para que sepa hasta donde tiene que llegar.
            ProjectileController projectile = enemyShot.GetComponent<ProjectileController>();
            projectile.destination = destination;
        }
        //Cada 0.2 segundos.
        yield return new WaitForSeconds(GenerationRateTime);

        //Se llama a sí misma para seguir generando
        StartCoroutine(GenerateEnemyShot());
    }

    /// <summary>
    /// Función para elegir un punto aleatorio como origen
    /// </summary>
    /// <returns>Posición del origen elegido</returns>
    private Vector3 DetermineOrigin()
    {
        Transform[] transforms = spawnsParent.GetComponentsInChildren<Transform>();

        int randomN = Random.Range(1, transforms.Length);

        return transforms[randomN].position;
    }

    /// <summary>
    /// Función para elegir un punto aleatorio como destino
    /// </summary>
    /// <returns>Posición del destino elegido</returns>
    private Vector3 DetermineObjective()
    {
        Transform[] transforms = objectivesParent.GetComponentsInChildren<Transform>();

        int randomN = Random.Range(1, transforms.Length);

        return transforms[randomN].position;
    }


}
