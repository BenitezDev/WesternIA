using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour
{

    #region Singleton
    public static CoverManager instance = null;
  
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)

            Destroy(gameObject);

    }
    #endregion

    /// <summary>
    /// List of exposed BallCovers
    /// </summary>
    public List<BallCover> dangerCovers = new List<BallCover>();

    /// <summary>
    /// List of safe BallCovers
    /// </summary>
    public List<BallCover> safeCovers   = new List<BallCover>();



    /// <summary>
    /// Given a position, it returns the closest cover
    /// </summary>
    /// <param name="enemyTr">Transform of which you want to know the nearest coverage</param>
    /// <returns>The closest BallCover given a Transform</returns>
    public BallCover getCloserSafeCover(Transform enemyTr)
    {
        BallCover closerBall = safeCovers[safeCovers.Count - 1];
        float minDistance = (enemyTr.position - safeCovers[safeCovers.Count-1].transform.position).sqrMagnitude;

        foreach (BallCover ball in safeCovers)
        {
            if (ball.isEmpty)
            {
                float auxDistance = (enemyTr.position - ball.transform.position).sqrMagnitude;

                if (auxDistance < minDistance)
                {
                    closerBall = ball;
                    minDistance = auxDistance;
                }

            }
        }

        return closerBall;
    }
   

    /// <summary>
    /// Move a CoverBall from SafeCovers List<> to dangerCovers List<>
    /// </summary>
    /// <param name="ball">BallCover that wants to be added in DangerCovers List<> </param>
    public void AddCoverBallToDangerCovers(BallCover ball)
    {
        RemoveCoverBallfronSafeCovers(ball);
        ball.isSafe = false;
        dangerCovers.Add(ball);
    }

    /// <summary>
    /// Remove a Cover ball from the safeCovers List<>
    /// </summary>
    /// <param name="ball">BallCover that wants to be removed from safeCovers List<> </param>
    public void RemoveCoverBallfronSafeCovers(BallCover ball)
    {
        safeCovers.Remove(ball);
    }

    /// <summary>
    /// Add a ball to the safeCovers List<>
    /// </summary>
    /// <param name="ball">BallCover that wants to be added in safeCovers List<> </param>
    public void MakeCoverToBeSafe (BallCover ball)
    {
        safeCovers.Add(ball);
    }

    /// <summary>
    /// Display in Console info about a ball
    /// </summary>
    /// <param name="ball">BallCover you want to display the information</param>
    private void printBallInfo(BallCover ball)
    {
        print("ID: " + ball.id + " CoverType: " + ball.coverType + "\nIs save: " + ball.isSafe + " Is empty: " + ball.isEmpty);
    }
    

}
