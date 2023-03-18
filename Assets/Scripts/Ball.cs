using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallUtilities
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody2D rb;
        public int boostForce = 100;
        public int directionalForce = 50;

        private void Start()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
        }
        public void Boost()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0, boostForce));
        }
        public void UpRight()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(directionalForce, boostForce));
        }
        public void UpLeft()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(-directionalForce, boostForce));
        }
        public void Left()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(-boostForce, 0));
        }
        public void Right()
        {
            rb = GameObject.Find("/Ball/Circle").GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(boostForce, 0));
        }
    }
}