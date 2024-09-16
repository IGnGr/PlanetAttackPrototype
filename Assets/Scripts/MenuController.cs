using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que se encarga de las transiciones en el men� principal para mostrar sus distintas secciones.
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Referencia al parent con los elementos del men� principal
    /// </summary>
    public GameObject menuSection;
    /// <summary>
    /// Referencia al parent con los elementos del apartado de cr�ditos
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
    /// Referencia al parent con los elementos del apartado de selecci�n de modo de juego
    /// </summary>
    public GameObject gameSelectSection;

    /// <summary>
    /// Funci�n para salir del juego
    /// </summary>
    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
    #endif
        Application.Quit();
    }

    /// <summary>
    /// Funci�n para comenzar la partida
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
    /// Funci�n para desactivar todas la secciones de UI
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
    /// Funci�n para mostrar la secci�n de cr�ditos
    /// </summary>
    public void ShowCredits()
    {
        DisableAllSections();
        creditsSection.SetActive(true);
    }

    /// <summary>
    /// Funci�n para mostrar la secci�n de opciones
    /// </summary>
    public void ShowOptions()
    {
        DisableAllSections();
        optionsSection.SetActive(true);
    }

    /// <summary>
    /// Funci�n para mostrar el men� principal
    /// </summary>
    public void ShowMenu()
    {
        DisableAllSections();
        menuSection.SetActive(true);
    }

    /// <summary>
    /// Funci�n para mostrar la secci�n de ayuda
    /// </summary>
    public void ShowHelp()
    {
        DisableAllSections();
        helpSection.SetActive(true);
    }
}
