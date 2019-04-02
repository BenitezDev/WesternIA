using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallCover : MonoBehaviour
{
  
    private static int globalId = 0;

    /// <summary>
    /// Boolean that depends on whether there is another enemy in the cover or not
    /// </summary>
    public bool isEmpty = true;

    /// <summary>
    /// Boolean that depends on whether the BallCover is exposed to the player or not
    /// </summary>
    public bool isSafe  = true;

    /// <summary>
    /// Unique Id of the instance of a BallCover
    /// </summary>
    public int id;

    /// <summary>
    /// Enum of the differents covers
    /// </summary>
    public enum CoverType { BigCover, SmallCover };

    /// <summary>
    /// CoverType (enum) of the BallCover
    /// </summary>
    public CoverType coverType;

    private void Awake()
    {   
        // Give a unique Id to each instance
        id = globalId++;
        name = (coverType + " " + id);
    }

    private void Start()
    {
        CoverManager.instance.safeCovers.Add(this);
    }

}
