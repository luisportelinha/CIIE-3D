using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;
    public PickUpController pickUpcontroller;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ConversationManager.Instance.StartConversation(conversation);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pickUpcontroller.PickUp();
               
                
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ConversationManager.Instance.EndConversation();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
