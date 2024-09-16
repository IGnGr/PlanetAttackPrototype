using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para gestionar el cambio de m�sica en la escena del juego
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto con la canci�n de fondo del juego
    /// </summary>
    [SerializeField] private GameObject gameScreenMusic;

    /// <summary>
    /// Referencia al objeto con la canci�n de la escena de puntuaci�n.
    /// </summary>
    [SerializeField] private GameObject finalScreenMusic;

    /// <summary>
    /// Reproduce la m�sica de juego
    /// </summary>
    public void PlayGameMusic()
    {
        StopAllMusic();
        gameScreenMusic.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Reproduce la musica de la pantalla de puntuaci�n
    /// </summary>
    public void PlayFinalScreenMusic()
    {
        StopAllMusic();
        finalScreenMusic.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Para toda la m�sica.
    /// </summary>
    public void StopAllMusic()
    {
        gameScreenMusic.GetComponent<AudioSource>().Stop();
        finalScreenMusic.GetComponent<AudioSource>().Stop();
    }
}
