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

    public void CambiarEscena(int escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego");
        //Application.Quit();
    }

    public void abrirOpciones()
    {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void cerrarOpciones()
    {
        SceneManager.UnloadSceneAsync(4);
    }

    public void abrirPausa()
    {
        SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
    }

    public void cerrarPausa()
    {
        SceneManager.UnloadSceneAsync(5);
    }
}
