using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pausa : MonoBehaviour
{
    public AudioSource fuenteAudio;
    public bool pausa = false;
    public InstanceGC gameController;

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
                fuenteAudio.Pause();
                gameController.abrirPausa();
            }
            else
            {
                pausa = false;
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                fuenteAudio.UnPause();        
                if (gameController.estaEnOpciones()){
                    gameController.cerrarOpciones();
                }
                gameController.cerrarPausa();     
            }
        }

        // Si se presiona la tecla L se cambia a la escena 6(CUANDO SE MUERE VAYA)
        if(Input.GetKeyDown(KeyCode.L)){
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameController.CambiarEscena(6);
        }

        // Si se presiona la V se cambia a la escena 7(CUANDO SE GANA VAYA)
        if(Input.GetKeyDown(KeyCode.V)){
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameController.CambiarEscena(7);
        }
        
    }

    public void volverMenuRestaurando()
    {
        Time.timeScale = 1;
        pausa = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameController.CambiarEscena(0);
    }


}
