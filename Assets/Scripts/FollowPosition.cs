using System;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] int numFollow;
    public Vector3 spotLoc = new Vector3(0f,0f,0f);
    public Quaternion spotRot;
    private string childName;
    [SerializeField] private Transform child;
    [SerializeField] private Transform parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childName = Convert.ToString(numFollow);
        child = parent.Find(childName);
        spotRot = child.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        spotLoc = child.position;
        spotRot = child.rotation;
        if (child == null)
        {
            Debug.Log("Child is null");
        }
    }
}
