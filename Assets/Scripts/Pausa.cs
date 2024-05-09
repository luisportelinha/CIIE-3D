using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pausa : MonoBehaviour
{
    public GameController gameController;
    public AudioSource audioPausa;
    public bool pausa = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si se presiona la tecla "P" o "Escape" se pausa el juego
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausa == false)
            {
                pausa = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                audioPausa.Pause();
                gameController.abrirPausa();
            }
            else
            {
                pausa = false;
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                audioPausa.UnPause();        
                gameController.cerrarPausa();     
            }
        }
        
    }
}
