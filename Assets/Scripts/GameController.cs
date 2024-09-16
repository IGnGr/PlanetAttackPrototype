using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase para gestionar el fin de la partida así como la vuelta al menú principal.
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto de la máxima puntuación guardada en disco.
    /// </summary>
    public TMP_Text highScoreSavedText;
    /// <summary>
    /// Referencia al objeto de la puntuación obtenida esta partida.
    /// </summary>
    public TMP_Text highScoreThisGameText;

    /// <summary>
    /// Referencia al GameObject con los objetos a mostrar al terminar la partida
    /// </summary>
    public GameObject WinScreen;

    /// <summary>
    /// Referencia al GameObject con los objetos de la partida y los cuales hay que desactivar
    /// </summary>
    public GameObject GameScreen;

    /// <summary>
    /// Referencia al playerController y que debe desactivarse
    /// </summary>
    public GameObject playerController;

    /// <summary>
    /// Referencia al enemyController y que debe desactivarse
    /// </summary>
    public GameObject enemyController;

    /// <summary>
    /// Referencia al audioController para controlar la música del juego.
    /// </summary>
    public AudioController audioController;

    /// <summary>
    /// Variable para determinar el modo de juego
    /// </summary>
    static public bool isTimeAttack = false;


    private void Start()
    {
        audioController.PlayGameMusic();
        if(SceneManager.GetActiveScene().name.Contains("Time"))
        {
            isTimeAttack = true;
            ScoreController.time = 30.0f;
            StartCoroutine(DecreaseTimer());
        }
        else
        {
            ScoreController.time = 0.0f;
        }
    }

    /// <summary>
    /// Función para cargar la escena del menú
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");

    }


    /// <summary>
    /// Gestiona el fin del juego
    /// </summary>
    public void EndGame()
    {
        audioController.PlayFinalScreenMusic();

        //Activa la pantalla de fin con el resultado y desactiva la de juego
        GameScreen.SetActive(false);
        WinScreen.SetActive(true);

        //Desactiva los controllers del jugador y enemigos
        playerController.SetActive(false);
        enemyController.SetActive(false);

        //Devuelve el cursor 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Obtenemos la puntuación de la partida que acaba de finalizar
        int scoreThisGame = ScoreController.score;

        int maxScoreSaved;
        //Obtenemos la maxima puntuación desde disco

        maxScoreSaved = isTimeAttack ?  PlayerPrefs.GetInt("MaxScoreTimeAttack") : PlayerPrefs.GetInt("MaxScore");



        //Si la puntuación de la partida es superior a la máxima guardada, la guardaremos.
        if (scoreThisGame > maxScoreSaved)
        {
            maxScoreSaved = scoreThisGame;

            if(isTimeAttack)
                PlayerPrefs.SetInt("MaxScoreTimeAttack", ScoreController.score);
            else
                PlayerPrefs.SetInt("MaxScore", ScoreController.score);

        }

        //Se muestran las puntuaciones
        highScoreSavedText.text = maxScoreSaved.ToString();
        highScoreThisGameText.text = scoreThisGame.ToString();

    }

    /// <summary>
    /// Decrementa el contador de tiempo
    /// </summary>
    IEnumerator DecreaseTimer()
    {


        yield return new WaitForSeconds(1);
        ScoreController.time--;

        //Se llama a sí misma para seguir actualizandose
        StartCoroutine(DecreaseTimer());
    }

    public static void IncreaseTimer(float time)
    {
        ScoreController.time += time;

    }
}
