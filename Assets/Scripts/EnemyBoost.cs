using BallUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBoost : MonoBehaviour
{
    public Ball ball;
    private void Awake()
    {
        if (ball == null) 
            ball = GameObject.Find("/Ball").GetComponent<Ball>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Circle")
            ball.Boost();
    }
}
