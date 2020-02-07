using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DropZone : MonoBehaviour
{
    public int TeamId;
    public Bounds Bounds;
    public BoxCollider boxCollider;
    
    
    private void Start() {
        Bounds = boxCollider.bounds;
        Debug.Log(Bounds);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Heavy Object")
        {
            var furnitureComponent = other.gameObject.GetComponent<Furniture>();
            if (furnitureComponent && !furnitureComponent.HasBeenRewarded)
            {
                if (IsCompletelyInside(other.gameObject))
                {
                    GivePoints(furnitureComponent);
                }

            }
        }    
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Heavy Object")
        {
            var furnitureComponent = other.gameObject.GetComponent<Furniture>();
            if (furnitureComponent && furnitureComponent.HasBeenRewarded)
            {
                RemovePoints(furnitureComponent);
                Debug.Log("Remove");
            }
        }  
    }

    private void GivePoints(Furniture furnitureComponent)
    {
        var givenPoints = furnitureComponent.GetPoints();
        var currentPoints = PointData.Instance.GetValue(PointData.ID_PREFIX + TeamId);
        var newScore = givenPoints + currentPoints;

        PointData.Instance.SetValue(PointData.ID_PREFIX + TeamId, newScore);
        furnitureComponent.Pose(newScore);
    }

    private void RemovePoints(Furniture furnitureComponent)
    {
        var currentPoints = PointData.Instance.GetValue(PointData.ID_PREFIX + TeamId);
        var newScore = currentPoints - furnitureComponent.PoseValue;

        PointData.Instance.SetValue(PointData.ID_PREFIX + TeamId, newScore);
        furnitureComponent.Dispose();
    }

    private bool IsCompletelyInside(GameObject other)
    {
        Bounds bounds = other.GetComponent<BoxCollider>().bounds;
        bool hasMin = Bounds.Contains(bounds.min);
        bool hasMax = Bounds.Contains(bounds.max);

        return hasMin && hasMax;
    }
}
