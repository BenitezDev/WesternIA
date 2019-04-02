using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCoverManager : MonoBehaviour
{
    /// <summary>
    /// The actual cover that the enemy is on
    /// </summary>
    BallCover currentCover;
    
    /// <summary>
    /// The square distance to start crouching
    /// </summary>
    public float sqrMagnitudeToCrouch = 2f;

    private EnemyLocomotion enemyLocomotion;
    private EnemyDecider    decider;

    private bool flagFirstTime; // to avoid null references


    void Start()
    {
        flagFirstTime = false;

        enemyLocomotion = GetComponent<EnemyLocomotion>();
        decider         = GetComponent<EnemyDecider>();
    }

    private void Update()
    {
        if (flagFirstTime && !currentCover.isSafe) // Player sees this enemy?
        {
            decider.EnemyIsInDanger();
        }
        else
        {
            decider.EnemyIsSafe();
        }
    }


    /// <summary>
    /// Return the closer safe cover of this enemy
    /// </summary>
    /// <returns></returns>
    public BallCover SearchCloserSafeCover()
    {
        if (currentCover != null)
        {
            currentCover.isEmpty = true;
        }

        currentCover = CoverManager.instance.getCloserSafeCover(this.transform);
        currentCover.isEmpty = false;

        flagFirstTime = true; 

        return currentCover;
    }



    /// <summary>
    /// Coroutine that controls when the enemy must crounch or stand up
    /// </summary>
    /// <param name="ct">CoverType of the BallCover</param>
    /// <returns></returns>
    public IEnumerator checkDistanceToCover(BallCover.CoverType ct)
    {
        if (ct == BallCover.CoverType.SmallCover)
        {
            bool arrive = false;
            while (!arrive && !enemyLocomotion.crouch)
            {
                //  Compare the square of the remaining distance to the square minimun distance to crouch
                if ((transform.position - currentCover.transform.position).sqrMagnitude <= sqrMagnitudeToCrouch)
                {
                    enemyLocomotion.Crouch();

                    arrive = true;
                }
                yield return null;
            }

        }
        else if (ct == BallCover.CoverType.BigCover)
        {
            if (enemyLocomotion.crouch)
            {
                // Wait a bit before the enemy stands
                yield return new WaitForSeconds(1f);

                enemyLocomotion.ResetCrouch();
            }
        }

        StopCoroutine(checkDistanceToCover(ct));
    }

}
