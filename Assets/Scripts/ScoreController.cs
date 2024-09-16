using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Clase que se encarga de mostrar en la UI el valor de puntos actual y la vida.
/// </summary>
public class ScoreController : MonoBehaviour
{
    /// <summary>
    /// Texto con la puntuación a mostrar
    /// </summary>
    [SerializeField] private TMP_Text scoreText;
    /// <summary>
    /// Texto con las vidas restantes
    /// </summary>
    [SerializeField] private TMP_Text livesText;

    /// <summary>
    /// Texto con el tiempo restante
    /// </summary>
    [SerializeField] private TMP_Text timeText;

    /// <summary>
    /// Variable que almacena el valor de la puntuación
    /// </summary>
    static public int score = 0;
    /// <summary>
    /// Variable que almacena el valor de las vidas restantes
    /// </summary>
    static public int lives = 5;

    /// <summary>
    /// Variable que almacena el valor del tiempo restante
    /// </summary>
    static public float time = 0;
    /// <summary>
    /// Referencia al GameController para la gestión del estado de la partida.
    /// </summary>
    private GameController gameController;




    void Start()
    {
        //Inicialización necesaria en caso de partidas consecutivas
        lives = 5;
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Se mantienen actualizados los valores respecto a sus variables
        scoreText.text = score.ToString();
        livesText.text = "X " + lives.ToString();

        if(GameController.isTimeAttack)
        {
            if (time > 0)
            {
                float minutes = time >= 60 ? time / 60 : 0;
                float secs = time % 60;

                string timeString = string.Format("{0:00}:{1:00}", minutes, secs);

                timeText.text = timeString;
            }

            else
            {
                gameController.EndGame();
            }
        }


    }


}
