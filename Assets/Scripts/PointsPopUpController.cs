using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Clase que se encarga de mostrar el valor correcto de los diálogos con los puntos generados en cada explosión
/// </summary>
public class PointsPopUpController : MonoBehaviour
{
    /// <summary>
    /// Referencia al texto a actualizar
    /// </summary>
    [SerializeField] private TMP_Text pointsText;


    /// <summary>
    /// Referencia al texto a actualizar
    /// </summary>
    [SerializeField] private TMP_Text timeText;

    /// <summary>
    /// Valor de los puntos a incrementar en el contador
    /// </summary>
    public int points = 0;

    /// <summary>
    /// Valor del tiempo a incrementar en el contador
    /// </summary>
    private float time = 3;


    private void Start()
    {
        //Mantiene el valor acorde a su miembro "points"
        pointsText.text = points.ToString();

        Debug.Log("points: " + points);
        if (GameController.isTimeAttack && points > 100)
        {
            timeText.text = time.ToString() + " Sec";
            GameController.IncreaseTimer(time);
        }
        else
        {
            timeText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }


}
