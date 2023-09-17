using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play: MonoBehaviour
{
    public string nextLevel;
    public void CambioEscena()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
