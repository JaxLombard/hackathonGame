using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterT1 : MonoBehaviour
{
    float xVel = 0;
    float yVel = 0;
    public Transform playerT;
    public Transform endPath;
    public Transform[] ePathArray;
    public GameObject exclam;
    public Rigidbody2D rb;
    float distancexPath = 0;
    float distancey = 0;
    public float parabTime = 0;
    public float maxParabTime = 5;
    bool parabHasStarted = false;
    int resetPathNum = 0;
    float speedUpTime = 0;
    bool hasSpotted = false;
    float totY = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // ///////////////////////////////////////////////////


    // ///////////////////////////////////////////////////
    float getDist(float V1, float V2)
    {
        float distanceX = V1 - V2;
        return distanceX;
    }

    // ///////////////////////////////////////////////////
    float ComputeXVel()
    {
        if (distancexPath < 0)
        {
            if (speedUpTime < 1)
                speedUpTime += Time.deltaTime/2;

            xVel = ((distancexPath / 4 - 2) * speedUpTime)/1.5f;
        }
        else
        {
            if (speedUpTime < 1)
                speedUpTime += Time.deltaTime/2;

            xVel = ((distancexPath / 4 + 2) * speedUpTime)/1.5f;
        }
        return xVel;
    }

    // ///////////////////////////////////////////////////
    float ComputeYVel()
    {
        totY += Time.deltaTime;
        yVel = Mathf.Cos(totY * 4)/2;
        if (hasSpotted && parabTime < 1)
        {
            parabTime += Time.deltaTime;
            yVel += (distancey * 1-parabTime)/3;
        }
        else
        {
            parabTime = 0;
        }


        return yVel;
    }

    // ///////////////////////////////////////////////////
    void resetPath()
    {
        if (Mathf.Abs(distancexPath) < 1)
        {
            speedUpTime = 0;
            resetPathNum += 1;
            endPath = ePathArray[resetPathNum % ePathArray.Length];
        }
    }

    // ///////////////////////////////////////////////////
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            hasSpotted = true;
            exclam.SetActive(true);
        }
    }

    // ///////////////////////////////////////////////////
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            exclam.SetActive(false);
            hasSpotted = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (distancexPath < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (distancexPath > 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        distancexPath = getDist(endPath.transform.position.x, transform.position.x);
        distancey = getDist(playerT.transform.position.y, transform.position.y);

        ComputeXVel();

        ComputeYVel();

        resetPath();

        rb.velocity = new Vector3(xVel, yVel, 0);
    }
}
