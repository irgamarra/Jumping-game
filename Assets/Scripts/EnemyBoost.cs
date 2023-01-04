using BallUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBoost : MonoBehaviour
{
    public Ball ball;
    [SerializeField]
    private Animator thisAnimator;
    private CircleCollider2D thisCollider;
    [SerializeField]
    private string boostAnimationName = "Boost";
    private void Awake()
    {
    }
    void Start()
    {
        if (ball == null)
            ball = GameObject.Find("/Ball").GetComponent<Ball>();
        if (thisAnimator == null)
            thisAnimator = this.GetComponent<Animator>();
        if (thisCollider == null)
            thisCollider = this.GetComponent<CircleCollider2D>();

    }
    void Update()
    {
        // To disable collider on animation "Boost"
        thisCollider.enabled = !thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Boost");

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Circle")
        { 
            ball.Boost();
            thisAnimator.SetBool("OnTriggerEnter", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Circle")
        {
            thisAnimator.SetBool("OnTriggerEnter", false);
        }
    }
}
