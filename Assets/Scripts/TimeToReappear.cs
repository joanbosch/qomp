﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToReappear : MonoBehaviour
{
    public float timeToReappear = 4.0f;
    private Rigidbody rb;
    private PlayerMovement pm;
    public Vector3 initialPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private bool calledToMoveCamera = false;

    private CamerasScript cs;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        cs = GameObject.Find("Camera").GetComponent<Level5Cameras>();

        if (cs == null) cs = GameObject.Find("Camera").GetComponent<Level4Cameras>();
        if (cs == null) cs = GameObject.Find("Camera").GetComponent<Level3Cameras>();
        if (cs == null) cs = GameObject.Find("Camera").GetComponent<Level2Cameras>();
        if (cs == null) cs = GameObject.Find("Camera").GetComponent<Level1Cameras>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToReappear -= Time.deltaTime;

        if ((timeToReappear <= 1.0f) && !calledToMoveCamera)
        {
            calledToMoveCamera = true;
            cs.moveCameraToOrigin();
        }

        if (timeToReappear <= 0.0f)
        {
            rb.MovePosition(initialPosition); // TODO: change to savepoint
            gameObject.transform.position = initialPosition;
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rb.rotation = Quaternion.identity;
            // 1 second to start moving
            if (timeToReappear <= -1.0f)
            {
                pm.resetVelocity();
                pm.resetDie();
                timeToReappear = 4.0f;
                calledToMoveCamera = false;
                this.enabled = false;
            }
        }
    }
}
