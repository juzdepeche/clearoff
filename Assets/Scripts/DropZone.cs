using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public int TeamId;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Heavy Object")
        {
            var furnitureComponent = other.gameObject.GetComponent<Furniture>();
            if (furnitureComponent)
            {
                var givenPoints = furnitureComponent.GetPoints();
                var currentPoints = PointData.Instance.GetValue(PointData.ID_PREFIX + TeamId);
                var newScore = givenPoints + currentPoints;

                PointData.Instance.SetValue(PointData.ID_PREFIX + TeamId, newScore);
            }
        }    
    }
}
