using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    #region Vision area variables
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    #endregion

    #region Mesh variables
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    #endregion

    /// <summary>
    /// LayerMask where are all the instances of BallCovers
    /// </summary>
    public LayerMask CoversLayerMask;
    /// <summary>
    /// LayerMask where are all the meshes where the enemies can cover
    /// </summary>
    public LayerMask ObstacleLayerMask;

 

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        
        StartCoroutine("FindTargetsWithDelay", 1f);
    }

    /// <summary>
    /// Coroutine that Look for visibles covers (players perspective)
    /// </summary>
    /// <param name="delay">How many seconds do you want to wait between searches</param>
    /// <returns></returns>
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true) // While Player is alive
        {
            yield return new WaitForSeconds(delay);

            FindVisibleTargets();
        }
    }



    /// <summary>
    /// Check which BallCovers the player can see
    /// </summary>
    private void FindVisibleTargets()
    {
        // Make a all cover to be safe
        foreach(BallCover cover in CoverManager.instance.dangerCovers)
        {
            cover.isSafe = true;
            CoverManager.instance.MakeCoverToBeSafe(cover);
        }

        CoverManager.instance.dangerCovers.Clear(); //< 
        

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, CoversLayerMask);

      
        for (int i = 0; i < targetsInViewRadius.Length; ++i)
        { 
        
            BallCover ballTarget = targetsInViewRadius[i].GetComponent<BallCover>();

            Vector3 dirToBallTarget = (ballTarget.transform.position - transform.position).normalized; // Expensive


            if (Vector3.Angle(transform.forward, dirToBallTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, ballTarget.transform.position); // Expensive

                if (!Physics.Raycast(transform.position, dirToBallTarget, dstToTarget, ObstacleLayerMask))
                {
                    ballTarget.isSafe = false;
                    
                    CoverManager.instance.AddCoverBallToDangerCovers(ballTarget);

                }
            }
          
        }
    }


   

    // Mesh
    #region Mesh stuff
    void LateUpdate()
    {
        DrawFieldOfView();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }


            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, ObstacleLayerMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }


    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
    #endregion
}
