using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleUIHelper : MonoBehaviour
{
    public Animator animator;

    // Actual console object
    public TMP_InputField console;
    
    public List<string> messages;
    public int messagesIndex;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleConsole();
        }
        
        HandleMessageLog();
    }

    public void HandleMessageInput()
    {
        if (console.text != "")
        {
            messages.Add(console.text);
        }

        console.text = "";
    }
    
    private void HandleMessageLog()
    {
        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        
        if (up || down) 
            if (messages.Count == 1)
                console.text = messages[0];
        
        if (up)
        {
            if (messages[messagesIndex + 1] == null) return;
            messagesIndex += 1;
            console.text = messages[messagesIndex];
        }
        if (down)
        {
            if (messages[messagesIndex - 1] == null) return;
            messagesIndex -= 1;
            console.text = messages[messagesIndex];
        }
    }
    
    public void ToggleConsole(bool state)
    {
        if (!animator.enabled) 
            animator.enabled = true;
        animator.SetBool("Fade", state);
    }

    public void ToggleConsole()
    {
        bool state = animator.GetBool("Fade"); 
        ToggleConsole(!state);
    }
}
