using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase para controlar la vida de las explosiones generadas por los disparos del jugador con su consecuente actualización de puntuación para el jugador. 
/// </summary>
public class ExplosionController : MonoBehaviour
{
    /// <summary>
    /// Indica en que punto de la cadena se encuentra. Se pueden encadenar explosiones entre sí y el indicador se incrementará acordemente.
    /// </summary>
    public int chainIndicator = 1;
    /// <summary>
    /// Puntos (base) que se sumarán a la puntuación del jugador. 
    /// </summary>
    public int pointsPerProjectileDestroyed = 100;
    /// <summary>
    /// Prefab de un pequeño indicador en forma de texto con los puntos obtenidos en la explosión, para informar al jugador.
    /// </summary>
    public GameObject pointsObtainedPrefab;

    /// <summary>
    /// Referencia al canvas de la escena
    /// </summary>
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Gestionamos y mostramos la informacion con los puntos obtenidos cuando se destruye el gameObject
    private void OnDestroy()
    {
        //Solamente mostramos el indicador con los puntos cuando es una explosión que ha destruido misiles enemigos
        if(pointsPerProjectileDestroyed * chainIndicator > 0)
        {
            //Por cada enemigo en la misma cadena, los puntos que dará se duplican
            int pointsObtained = pointsPerProjectileDestroyed * (int)Math.Pow((double)2, (double)chainIndicator - 1);

            //Actualizamos la puntuación en nuestro ScoreController
            ScoreController.score += pointsObtained;

            //Instanciamos el popUp con la puntuación obtenida
            GameObject pointsPopUp = Instantiate(pointsObtainedPrefab, gameObject.transform.position + canvas.transform.position, Quaternion.identity, canvas.transform);
            pointsPopUp.GetComponent<PointsPopUpController>().points = pointsObtained;
            Destroy(pointsPopUp, 0.75f);
        }

    }
}
