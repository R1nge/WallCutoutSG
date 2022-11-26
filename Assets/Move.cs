using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minPosX, maxPosX;
    private Vector3 _pos;
    private bool _isMovingLeft;

    private void Awake()
    {
        _pos = new Vector3 {
                x = Random.Range(0, 1) == 0 ? minPosX : maxPosX, 
                y = transform.position.y, 
                z = transform.position.z 
        };
        
        transform.position = _pos;
        
        if (Math.Abs(transform.position.x - minPosX) < .1f) { _isMovingLeft = false; }
        else if (Math.Abs(transform.position.x - maxPosX) < .1f) { _isMovingLeft = true; }
    }

    private void Update()
    {
        if (Math.Abs(transform.position.x - minPosX) < .1f) { _isMovingLeft = false; }
        else if (Math.Abs(transform.position.x - maxPosX) < .1f) { _isMovingLeft = true; }

        if (_isMovingLeft) { _pos.x = transform.position.x - speed * Time.deltaTime; }
        else { _pos.x = transform.position.x + speed * Time.deltaTime; }

        transform.position = _pos;
    }
}