using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Node
{
    
    private Node _parent;
    private BallCover _cover;
    private EnemyDecider.EnemyStates _action;

    private float _g;    // Real cost
    private float _f;    // Estimated cost


    public Node(Node p, EnemyDecider.EnemyStates a, BallCover cover ,float g)
    {
        this._parent = p;
        this._action = a;
        
        if (p != null)
        {
            this._g = p._g + g;
        }

        this._cover = cover;

    }

    // FStar to kill the player... ATM FStar is to fire in a safe zone
    private float FStar(/* Dont know*/)
    {
        // TODO: implement this
        return this._g;
    }
}

