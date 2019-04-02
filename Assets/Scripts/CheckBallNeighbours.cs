using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallNeighbours : MonoBehaviour
{

	public BallCover[] allCovers;
	
	[SerializeField] private  float minDistanceToBeNeighbours = 12;
	public static float maxDistance = 0;
	
	private void Start()
	{
		this.allCovers = GetComponentsInChildren<BallCover>();
		CheckDistanceToNeighbours();
		Debug.Break();
	}

	private void CheckDistanceToNeighbours()
	{
		for (var i = 0; i < this.allCovers.Length; i++)
		{
			var currentCover = this.allCovers[i];
			
			for (int j = 0; j < this.allCovers.Length; j++)
			{
				
				// Distance <= minDistanceToBeNeighbours
				var distance = 
					Vector3.Distance(currentCover.transform.position, this.allCovers[j].transform.position);
				if (distance > maxDistance) maxDistance = distance;
				
				if ( distance <= this.minDistanceToBeNeighbours)
				{
					// Not the same cover
					if (currentCover.id != this.allCovers[j].id)
					{
//							//List of Neighbours:
//						 
//							// Not included before
//							if (!currentCover.Neighbours.Contains(this.allCovers[j]))
//							{
//								currentCover.Neighbours.Add(this.allCovers[j]);
//							}
						

						// Not in Dictionary
						if (!currentCover.DicNeighbours.ContainsKey(this.allCovers[j]))
						{
							currentCover.DicNeighbours.Add(this.allCovers[j],distance);
						}
					}
				}
			}
		}

		
	}
}
