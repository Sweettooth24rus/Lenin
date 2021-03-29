using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonClick : MonoBehaviour
{

    public void Play() {
        Debug.Log("knopka rabotaet");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
