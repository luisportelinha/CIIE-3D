using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionLogicController : MonoBehaviour
{
    public OptionsController optionsController;
    // Start is called before the first frame update
    void Start()
    {
        optionsController = GameObject.FindGameObjectWithTag("opciones").GetComponent<OptionsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarOpciones();
        }
    }

    public void MostrarOpciones()
    {
        optionsController.screenOptions.SetActive(true);
    }
}
