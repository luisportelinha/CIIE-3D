using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        
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
        Application.Quit();
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

    public bool estaEnOpciones()
    {
        return SceneManager.GetSceneByBuildIndex(4).isLoaded;
    }

    public void cerrarPausa()
    {
        SceneManager.UnloadSceneAsync(5);
    }

    public void mensaje(string mensaje)
    {
        Debug.Log(mensaje);
    }
}
