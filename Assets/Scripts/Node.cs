using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;

public class Node : IComparable<Node>
{
    private Node _parent;
    private BallCover _cover;
    private EnemyDecider _decider;

    private float _g; // Real cost
    private float _f; // Estimated cost


    public Node(Node p, BallCover cover, EnemyDecider decider, float f)
    {
        this._parent = p;
        this._decider = decider;

        if (p != null)
        {
            this._g = p._g + f;
        }

        this._cover = cover;
    }

    public BallCover getBallCover()
    {
        return this._cover;
    }

    public List<Node> Expand()
    {
        var sucesores = new List<Node>();
        var vecinos = _cover.DicNeighbours;

        // Debug.Log(_cover.DicNeighbours.ToString());


        foreach (var vecino in vecinos)
        {
            // sin ciclos simples ATM

            if (!vecino.Key.isEmpty) break;

            var tempCost = 0f;

            if (!vecino.Key.isSafe) tempCost += this._decider.costs.playerSeeMe;

            var nuevoNodo = new Node(this, vecino.Key, this._decider, vecino.Value + tempCost);
            sucesores.Add(nuevoNodo);
        }


        return sucesores;
    }


    public bool esMeta()
    {
        return Aprox(this._cover.distanceToPlayer,
            this._decider.handleShoot.distanciaOptima,
            this._decider.costs.epsilonCost) && this._cover.isSafe && this._cover.isEmpty;


    }


    private static bool Aprox(float cost, float desireCost, float epsilon)
    {
        return (Mathf.Abs(cost - desireCost) <= epsilon);
    }

    // FStar to kill the player... ATM FStar is to fire in a safe zone... o that is what i want :)
    private float FStar( /* Dont know*/)
    {
        // TODO: implement this
        return this._g;
    }

    public Node GetPadre()
    {
        return this._parent;
    }

    public float getF()
    {
        return this._f;
    }


    public int CompareTo(Node other)
    {
        if (other._f >= this._f)
            return 1;
        else if (other._f < this._f)
            return -1;
        else return 0;
    }
}