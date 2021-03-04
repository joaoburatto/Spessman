using System;
using System.Collections;
using System.Collections.Generic;
using Spessman.Inventory.Extensions;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HandIKManager : MonoBehaviour
{
    public TwoBoneIKConstraint[] handIKs;
    public MultiAimConstraint[] holdIKs;
    
    private int selectedHandIKIndex;
    
    public Hands hands;

    private void Start()
    {
        if (hands == null) hands = transform.root.GetComponent<Hands>();
        
        hands.HandChanged += delegate(int i) 
        { 
            selectedHandIKIndex = i;
        };
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == selectedHandIKIndex)
            {
                float weight = handIKs[i].weight;
                if (Input.GetKey(KeyCode.Space))
                {
                    handIKs[i].weight = Mathf.LerpUnclamped(weight, 1, Time.deltaTime * 10);
                    holdIKs[i].weight = Mathf.LerpUnclamped(weight, 1, Time.deltaTime * 10);
                }
                else
                {
                    handIKs[i].weight = Mathf.LerpUnclamped(weight, 0, Time.deltaTime * 10);
                    holdIKs[i].weight = Mathf.LerpUnclamped(weight, 0, Time.deltaTime * 10);
                }
            }
            else
            {
                float weight = handIKs[i].weight;
                handIKs[i].weight = Mathf.LerpUnclamped(weight, 0, Time.deltaTime * 10); 
                holdIKs[i].weight = Mathf.LerpUnclamped(weight, 0, Time.deltaTime * 10);
            }
        }
    }
}
