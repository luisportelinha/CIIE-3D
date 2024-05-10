using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSettings : MonoBehaviour
{
    public PlayerController fpsMovement;
    public Arrow gun;

    Canvas canvas;
    bool active = false;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !active)
        {
            canvas.enabled = true;
            Invoke("Switch", 0.1f);

            //cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            //stop shooting
            gun.shootingEnabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && active)
        {
            canvas.enabled = false;
            Invoke("Switch", 0.1f);

            //cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            //enable shooting
            gun.shootingEnabled = true;
        }
    }
    private void Switch()
    {
        active = !active;
    }
}
