using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PasarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CambiarEscena0()
    {
        SceneManager.LoadScene(0);
    }

    public void CambiarEscena1()
    {
        SceneManager.LoadScene(1);
    }

    public void CambiarEscena2()
    {
        SceneManager.LoadScene(2);
    }

    public void CambiarEscena3()
    {
        SceneManager.LoadScene(3);
    }
}
