using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComprobarScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Encuentra todos los AudioListeners en la escena
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // Si hay más de un AudioListener, destruye los adicionales
        if (audioListeners.Length > 1)
        {
            for (int i = 1; i < audioListeners.Length; i++)
            {
                Destroy(audioListeners[i].gameObject);
            }
        }

        // Encuentra todos los EventSystems en la escena
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

        // Si hay más de un EventSystem, destruye los adicionales
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)
            {
                Destroy(eventSystems[i].gameObject);
            }
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
