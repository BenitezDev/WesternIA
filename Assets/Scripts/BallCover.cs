using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class BallCover : SerializedMonoBehaviour
{
    private static int globalId = 0;

    /// <summary>
    /// Boolean that depends on whether there is another enemy in the cover or not
    /// </summary>
    public bool isEmpty = true;

    /// <summary>
    /// Boolean that depends on whether the BallCover is exposed to the player or not
    /// </summary>
    public bool isSafe = true;

    /// <summary>
    /// Unique Id of the instance of a BallCover
    /// </summary>
    public int id;

    public float distanceToPlayer;

    /// <summary>
    /// Enum of the differents covers
    /// </summary>
    public enum CoverType
    {
        BigCover,
        SmallCover
    };

    /// <summary>
    /// CoverType (enum) of the BallCover
    /// </summary>
    public CoverType coverType;


    //public List<BallCover> Neighbours;

    /// <summary>
    /// Neighbours of this cover
    /// </summary>
    public Dictionary<BallCover, float> DicNeighbours = new Dictionary<BallCover, float>();

    private void Awake()
    {
        // Give a unique Id to each instance
        id = globalId++;
        name = (coverType + " " + id);
    }

    private void Start()
    {
        CoverManager.instance.safeCovers.Add(this);
        InvokeRepeating("CheckDistanceToPlayer",Random.Range(0,1), 1);


    }

    private void CheckDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, PCLocomotion.PlayerPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        // 12 is the Neighbours distance
        Gizmos.DrawWireSphere(transform.position, 12);
    }
}