using System;
using System.Collections;
using System.Collections.Generic;
using Spessman;
using TMPro;
using UnityEngine;

public class ConsoleUIHelper : MonoBehaviour
{
    public Animator animator;

    // Actual console object
    public TMP_InputField console;
    
    public List<string> messages;
    public int messagesIndex;

    public static event System.Action OnConsoleEnabled;
    public static event System.Action OnConsoleDisabled;
    
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
        string input = console.text;
        if (input != "")
        {
            messages.Add(input);
        }

        ConsoleCommandHandler.singleton.Command(input);
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
        CursorManager.singleton.SetCursorState(state);
        if (state)
        {
            console.Select();
            OnConsoleEnabled.Invoke();   
        }
        else
        {
            OnConsoleDisabled.Invoke();
        }
    }

    public void ToggleConsole()
    {
        bool state = animator.GetBool("Fade"); 
        ToggleConsole(!state);
    }
}
