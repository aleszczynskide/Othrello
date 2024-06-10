using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointerLogic : MonoBehaviour
{

    public GameObject Slajd1, Slajd2;
    public void NextSlajd()
    {
        Slajd1.SetActive(false);
    }
    public void NextSlajd2()
    {
        Slajd2.SetActive(false);
    }

    public void NextSlajd3()
    {
        Destroy(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
