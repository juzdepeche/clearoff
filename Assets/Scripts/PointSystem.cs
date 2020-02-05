using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    int teamCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(2);
        teamCount = GameController.Instance.GetTeamCount();
        for (int i = 0; i < teamCount; i++)
        {
            PointData.Instance.SetValue(PointData.ID_PREFIX + (i + 1), 0);
            PointData.Instance.AddObserver(UpdateGUIScore, PointData.ID_PREFIX + (i + 1));
        }
    }

    void UpdateGUIScore(int newPoint, string key)
    {
        Debug.Log(key + " " + newPoint);
    }
}
