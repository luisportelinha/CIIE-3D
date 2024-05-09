using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceGC : MonoBehaviour{
    void Start()
    {
    }

    void Update()
    {
    }

    public void PasarEscena()
    {
        GameController.instance.PasarEscena();
    }

    public void CambiarEscena(int escena)
    {
        GameController.instance.CambiarEscena(escena);
    }

    public void Salir()
    {
        GameController.instance.Salir();
    }


    public void cerrarOpciones()
    {GameController.instance.cerrarOpciones();
    }

    public void abrirOpciones()
    {GameController.instance.abrirOpciones();
    }

    public void abrirPausa()
    {GameController.instance.abrirPausa();
    }

    public void cerrarPausa()
    {GameController.instance.cerrarPausa();
    }

    public bool estaEnOpciones()
    {
        return GameController.instance.estaEnOpciones();
    }

}