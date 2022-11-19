using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Transform post1, post2;
    public float speed = 1f;
    public Transform StartPost;
    Vector3 NextPost;
    // Start is called before the first frame update
    void Start()
    {
        NextPost = StartPost.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextPost, speed * Time.deltaTime);
        if (transform.position == post1.position)
        {
            NextPost = post2.position;
        }
        if(transform.position == post2.position)
        {
            NextPost = post1.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(post1.position, post2.position);
    }
}
