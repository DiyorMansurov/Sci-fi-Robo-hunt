using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour
{
    public static CoverManager Instance;
    
    [SerializeField] private GameObject _coverPointParent;
    private List<Transform> _coverPoints = new List<Transform>();

    private void Awake()
    {
        Instance = this;

        foreach (Transform child in _coverPointParent.transform)
        {
            _coverPoints.Add(child);
        }
    }

    public Vector3 GetRandomCoverPoint()
    {
        int i = Random.Range(0, _coverPoints.Count);
        return _coverPoints[i].position;
    }
}
