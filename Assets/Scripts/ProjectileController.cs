using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Clase que se encarga de la l�gica de los proyectiles. Tanto de colisiones como de la vida del objeto.
/// </summary>
public class ProjectileController : MonoBehaviour
{
    /// <summary>
    /// Velocida de movimiento del proyectil
    /// </summary>
    public float speed = 1f;
    /// <summary>
    /// Velocidad de movimiento m�nima a la cual se puede mover el proyectil
    /// </summary>
    public float minSpeed = 1f;
    /// <summary>
    /// Velocidad de movimiento m�xima a la cual se puede mover el proyectil
    /// </summary>
    public float maxSpeed = 20f;
    /// <summary>
    /// Referencia al rigidBody del proyectil
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// Referencia al efecto que se mostrar� al generar el disparo
    /// </summary>
    public GameObject shootEffect;
    /// <summary>
    /// Referencia al efecto que se mostrar� al explotar el proyectil
    /// </summary>
    public GameObject hitEffect;
    /// <summary>
    /// Referencia al efecto que se muestra al explotar el proyectil por ser alcanzado por un disparo del jugador
    /// </summary>
    public GameObject DestroyedByPlayerEffect;
    /// <summary>
    /// Posici�n destino a la cual se dirige el proyectil
    /// </summary>
    public Vector3 destination;
    /// <summary>
    /// Distancia a la cual el proyectil considera que ha llegado al objetivo y explotar�.
    /// </summary>
    private float distanceToObjective = 0.1f;
    /// <summary>
    /// Indicador de la cadena actual. Las explosiones generadas por el jugador con un mismo disparo van aumentando su valor.
    /// </summary>
    int chainIndicator = 0;
    /// <summary>
    /// Puntos base que se proporcionan al jugador por lograr alcanzar al proyectil y hacer que explote.
    /// </summary>
    int pointsPerProjectileDestroyed = 100;
    /// <summary>
    /// Referencia al GameController para la gesti�n del estado de la partida.
    /// </summary>
    private GameController gameController;

    // Start is called before the first frame update
    void Start() {
        //Inicializaci�n de variables
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();

        //Se genera una velocidad en la franja [minSpeed,maxSpeed] y se fija en el rigidBody
        speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = transform.up * speed;
    
        //Se genera el efecto de disparo
        GameObject obj = (GameObject)Instantiate(shootEffect, transform.position - new Vector3(0, 0, 5), Quaternion.identity); 

        //Se fija la destrucci�n del proyectil en caso de que no sea posible llegar a su posici�n objetivo
        Destroy(gameObject, 15f); //Bullet will despawn after 15 seconds

    }

    private void Update()
    {
        //Se comprueba si se ha llegado al objetivo, si es as�, se detona
        if(Vector3.Distance(transform.position, destination) < distanceToObjective)
        {
            Explode();
        }
    }

    //Gesti�n de colisiones
    void OnTriggerEnter2D(Collider2D col)
    {
        //Colisiones entre proyectiles aliado y enemigo. Para el caso del disparo aliado.
        if ( gameObject.CompareTag("AlliedProjectile") && col.gameObject.CompareTag("Projectile"))
        {

            hitEffect = DestroyedByPlayerEffect;
            Explode();

        }else
        //Colisiones entre proyectiles aliado y enemigo. Para el caso del disparo enemigo
        if (gameObject.CompareTag("Projectile") && col.gameObject.CompareTag("AlliedProjectile"))
        {
            //Se actualiza la animaci�n para usar la de explosi�n aliada
            hitEffect = DestroyedByPlayerEffect;
            //Al enlazarse la explosi�n con otra, se considera que aumenta la cadena de explosiones, se actualiza el indice
            chainIndicator = 1;
            //Se detona.
            Explode();
        }
        else
        //Colisiones entre proyectil enemigo y una explosi�n ya en proceso
        if (gameObject.CompareTag("Projectile") && col.gameObject.CompareTag("Explosion"))
        {
            //Se actualiza la animaci�n para usar la de explosi�n aliada
            hitEffect = DestroyedByPlayerEffect;
            //Al enlazarse la explosi�n con otra, se considera que aumenta la cadena de explosiones, se actualiza el indice
            chainIndicator = col.gameObject.GetComponent<ExplosionController>().chainIndicator + 1;
            //Se detona.
            Explode();
        }else

        //Colisiones del proyectil enemigo con nuestras fuentes de energia (vidas) 
        if (gameObject.CompareTag("Projectile") && col.gameObject.CompareTag("Energy"))
        {
            ScoreController.lives--;

            //Si no quedan vidas, se pide a GameController que gestione el fin de partida
            if (ScoreController.lives == 0)
            {
                gameController.EndGame();
            }

            //Se fijan a 0 los puntos para que no se genere el texto con los puntos obtenidos
            pointsPerProjectileDestroyed = 0;
            //Se desactiva el GameObject para ver como se ha perdido la vida
            col.gameObject.SetActive(false);
            //Se detona
            Explode();

        }
    }

    /// <summary>
    /// Gestiona la detonaci�n del proyectil.
    /// </summary>
    void Explode()
    {
        //Generamos la explosion
        GameObject explosion = Instantiate(hitEffect, transform.position, Quaternion.identity);

        //Si es una explosi�n por causa de las explosiones del jugador se deber�n dar puntos y actualizar la cadena , por lo que se actualiza 
        if(hitEffect == DestroyedByPlayerEffect)
        {
            explosion.GetComponent<ExplosionController>().chainIndicator = chainIndicator;
            explosion.GetComponent<ExplosionController>().pointsPerProjectileDestroyed = pointsPerProjectileDestroyed;
        }
        
        Destroy(gameObject);
    }
}
