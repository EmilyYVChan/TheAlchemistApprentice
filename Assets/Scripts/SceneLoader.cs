using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    public void loadScene(int sceneNumber)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
    }
}