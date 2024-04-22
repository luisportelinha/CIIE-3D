using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public GameObject screenOptions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        screenOptions.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        screenOptions.SetActive(false);
        Time.timeScale = 1;
    }
}
