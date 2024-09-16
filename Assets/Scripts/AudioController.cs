using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para gestionar el cambio de música en la escena del juego
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto con la canción de fondo del juego
    /// </summary>
    [SerializeField] private GameObject gameScreenMusic;

    /// <summary>
    /// Referencia al objeto con la canción de la escena de puntuación.
    /// </summary>
    [SerializeField] private GameObject finalScreenMusic;

    /// <summary>
    /// Reproduce la música de juego
    /// </summary>
    public void PlayGameMusic()
    {
        StopAllMusic();
        gameScreenMusic.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Reproduce la musica de la pantalla de puntuación
    /// </summary>
    public void PlayFinalScreenMusic()
    {
        StopAllMusic();
        finalScreenMusic.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Para toda la música.
    /// </summary>
    public void StopAllMusic()
    {
        gameScreenMusic.GetComponent<AudioSource>().Stop();
        finalScreenMusic.GetComponent<AudioSource>().Stop();
    }
}
