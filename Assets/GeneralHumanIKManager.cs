using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spessman.Inventory.Extensions;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GeneralHumanIKManager : MonoBehaviour
{
    public TwoBoneIKConstraint[] handIKs;
    public MultiAimConstraint[] holdIKs;
    public FollowPosition[] followPosition;
    
    public Animator[] animator;
    private int selectedHandIKIndex;

    public Animator[] helpers;
    public Hands hands;

    private void Start()
    {
        if (hands == null) hands = transform.root.GetComponent<Hands>();
        
        hands.HandChanged += delegate(int i) 
        { 
            selectedHandIKIndex = i;
        };
    }

    public void PickupAnimationHelper(Vector3 position, float duration)
    {
        Transform target = handIKs[selectedHandIKIndex].data.target;

        foreach (Animator animator in helpers)
        {
            animator.speed = 1 / (duration / 2);
            animator.SetBool("Lock" ,true);
        }
        
        followPosition[selectedHandIKIndex].currentTarget = position;
        followPosition[selectedHandIKIndex].blocked = true;
        animator[selectedHandIKIndex].speed = 1 / (duration / 2);
        animator[selectedHandIKIndex].SetBool("Lock" ,true);

        target.position = position;
        
    }

    public void UnlockHandIK()
    {
        animator[selectedHandIKIndex].SetBool("Lock" , false);
        foreach (Animator animator in helpers)
        {
            animator.SetBool("Lock" ,false);
        }

        followPosition[selectedHandIKIndex].currentTarget = Vector3.zero;
        followPosition[selectedHandIKIndex].blocked = false;
    }
}
