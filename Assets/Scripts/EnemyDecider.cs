using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecider : MonoBehaviour
{

    enum EnemyStates { SearchCover, Fire, RunToCover, LookForBetterWeapon };

    /// <summary>
    /// Current state of this enemy
    /// </summary>
    EnemyStates enemyState;

    /// <summary>
    /// Whether this enemy is safe or not. ATM being in the player's line of sight
    /// </summary>
    [SerializeField]
    private bool imSafe;

    private EnemyCoverManager coverManager;
    private EnemyLocomotion   locomotion;


    void Start ()
    {
        coverManager = GetComponent<EnemyCoverManager>();
        locomotion   = GetComponent<EnemyLocomotion>();
     
        // The enemy start by moving to a save cover
        Invoke("MoveToNearestSaveCover", 1f);
	}


    // ATM the enemy only run away from the player
    // TODO: implement other behaviors
    void Update ()
    {
        if (imSafe)
        {
            // Fire
        }
        else
        {
            MoveToNearestSaveCover();
        }
       
	}

    /// <summary>
    /// Move this enemy to the nearest safe cover
    /// </summary>
    private void MoveToNearestSaveCover()
    {
        BallCover CoverToMoveTo = coverManager.SearchCloserSafeCover();

        locomotion.MoveTo( CoverToMoveTo.transform.position );

        // This make the enemy crouch if need it
        coverManager.StartCoroutine(coverManager.checkDistanceToCover(CoverToMoveTo.coverType));
    }

    
    public void EnemyIsInDanger()
    {
        imSafe = false;
    }
    public void EnemyIsSafe()
    {
        imSafe = true;
    }

}
