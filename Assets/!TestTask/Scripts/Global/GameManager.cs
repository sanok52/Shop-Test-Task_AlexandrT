using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winGO;

    public void Win()
    {
        winGO.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
