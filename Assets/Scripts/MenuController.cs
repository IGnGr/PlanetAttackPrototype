using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que se encarga de las transiciones en el menú principal para mostrar sus distintas secciones.
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Referencia al parent con los elementos del menú principal
    /// </summary>
    public GameObject menuSection;
    /// <summary>
    /// Referencia al parent con los elementos del apartado de créditos
    /// </summary>
    public GameObject creditsSection;
    /// <summary>
    /// Referencia al parent con los elementos del apartado de opciones
    /// </summary>
    public GameObject optionsSection;
    /// <summary>
    /// Referencia al parent con los elementos del apartado de como jugar
    /// </summary>
    public GameObject helpSection;

    /// <summary>
    /// Referencia al parent con los elementos del apartado de selección de modo de juego
    /// </summary>
    public GameObject gameSelectSection;

    /// <summary>
    /// Función para salir del juego
    /// </summary>
    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
    #endif
        Application.Quit();
    }

    /// <summary>
    /// Función para comenzar la partida
    /// </summary>
    public void Play()
    {
        DisableAllSections();
        gameSelectSection.SetActive(true);
    }

    public void PlayTime()
    {
        SceneManager.LoadScene("GameTime");
    }

    public void PlayArcade()
    {
        SceneManager.LoadScene("GameArcade");
    }


    /// <summary>
    /// Función para desactivar todas la secciones de UI
    /// </summary>
    public void DisableAllSections()
    {
        gameSelectSection.SetActive(false);
        menuSection.SetActive(false);
        creditsSection.SetActive(false);
        optionsSection.SetActive(false);
        helpSection.SetActive(false);
    }

    /// <summary>
    /// Función para mostrar la sección de créditos
    /// </summary>
    public void ShowCredits()
    {
        DisableAllSections();
        creditsSection.SetActive(true);
    }

    /// <summary>
    /// Función para mostrar la sección de opciones
    /// </summary>
    public void ShowOptions()
    {
        DisableAllSections();
        optionsSection.SetActive(true);
    }

    /// <summary>
    /// Función para mostrar el menú principal
    /// </summary>
    public void ShowMenu()
    {
        DisableAllSections();
        menuSection.SetActive(true);
    }

    /// <summary>
    /// Función para mostrar la sección de ayuda
    /// </summary>
    public void ShowHelp()
    {
        DisableAllSections();
        helpSection.SetActive(true);
    }
}
