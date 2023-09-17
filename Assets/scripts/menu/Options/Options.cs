using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public string nextLevel;
    public void CambioEscena()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
