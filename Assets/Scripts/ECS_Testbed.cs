using Assets.Scripts.Classes;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class ECS_Testbed : MonoBehaviour
{
    private List<ZMoveableObject> _moveableObjects;

    [SerializeField]
    public Transform ObjectToMove;
    [SerializeField]
    public int NumberOfObjects = 1000;
    [SerializeField]
    public int XSpread = 10;
    [SerializeField]
    public int YSpread = 10;
    [SerializeField]
    public int ZSpread = 10;

    private void Start()
    {
        _moveableObjects = new List<ZMoveableObject>();

        for (var i = 0; i < NumberOfObjects; i++)
        {
            Transform instance = Instantiate(
                ObjectToMove,
                new Vector3(UnityEngine.Random.Range(-XSpread, XSpread),
                            UnityEngine.Random.Range(-YSpread, YSpread),
                            UnityEngine.Random.Range(-ZSpread, ZSpread)), Quaternion.identity);

            _moveableObjects.Add(new ZMoveableObject
            {
                Transform = instance,
                Velocity = UnityEngine.Random.Range(0.1f, 1.0f)
            });
        }
    }

    private void Update()
    {
        float startTime = Time.realtimeSinceStartup;

        for (var i = 0; i < _moveableObjects.Count; i++)
        {
            _moveableObjects[i].Transform.position += new Vector3(0f, 0f, -_moveableObjects[i].Velocity * Time.deltaTime);
            Helpers.AddDummyHeavyTask();
        }

        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }

    public class ZMoveableObject
    {
        public Transform Transform;
        public float Velocity;
    }
}