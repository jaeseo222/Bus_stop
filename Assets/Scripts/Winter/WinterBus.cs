﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinterBus : MonoBehaviour{

    public WheelCollider[] colls = new WheelCollider[4]; //바퀴가 돌아가는 걸 표현하기위한 메쉬
    public Transform[] tires = new Transform[4];
    Rigidbody rb;
    Animation ani;
    
    private float velocity;
    public float accel; //0.01씩 증가가 기본, 유니티 씬에서 받아오는 값 (맵마다 다른 값)
    public static float speed;
    public static int sss; // 스크린에 비춰지는 속도값

    private float timecheck; //속도 떨어뜨릴때 타임체크
    private bool breaks = false;
    public TextMeshProUGUI speedT;
    private bool icecheck = true; //겨울씬에서 얼음에 닿았는지 체크

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //리지드바디를 받아온다.
        ani = GetComponent<Animation>(); 
        rb.centerOfMass = new Vector3(0, 0, 0); //무게중심을 가운데로 맞춰서 안정적으로 주행하도록 한다.*/
        velocity = 0;
        speed = 0;
    }

    private void Update()
    {
        //버스 이동
        transform.Translate(Vector3.forward * 0.01f * -speed);

        //버스 바퀴 회전
        for (int i=0;i<4;i++)  { 
            tires[i].Rotate(Vector3.right * -speed);
            if (colls[i].rpm > 1) {
                colls[i].brakeTorque = Mathf.Infinity;
            }
            if (i%2==1){
                
                if (Math.Abs(speed) < 5)  colls[i].steerAngle = 3 * Input.GetAxis("Horizontal") * Math.Abs(speed);
                else colls[i].steerAngle = 5 * Input.GetAxis("Horizontal") * 3;
                Vector3 position;
                Quaternion rotation;
                //Vector3 rot;
                colls[i].GetWorldPose(out position, out rotation);
                //rot = tires[i].eulerAngles + rotation.eulerAngles;
                tires[i].rotation = rotation;
            }


        }

        // 버스 차체 회전
        if (icecheck){ //얼음에 닿지 않았을 때 실행
            if(Input.GetKey(KeyCode.A) && speed != 0)
            {
                if (Math.Abs(speed) < 7)  transform.Rotate(Vector3.up * 0.15f * -speed);
                else if (speed > 0) transform.Rotate(Vector3.up * 0.15f * -7);
                else if (speed < 0) transform.Rotate(Vector3.up * 0.15f * 7);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                if (Math.Abs(speed) < 7)  transform.Rotate(Vector3.up * 0.1f * speed);
                else if (speed > 0) transform.Rotate(Vector3.up * 0.1f * 7);
                else if (speed < 0) transform.Rotate(Vector3.up * 0.1f * -7);        
            }
        }
        

        //급정거
        if (Math.Abs(speed) > 0 && Input.GetKeyDown(KeyCode.Space)){
            breaks = true;
            rb.AddRelativeForce(new Vector3(0, -1, 0) * 1000 * Math.Abs(speed));
            rb.AddRelativeForce(new Vector3(0, 0, -1) * 50000 * Math.Abs(speed));
            //rb.AddRelativeForce(new Vector3(0, 0, 1) * 30000 * Math.Abs(speed));
        }

        if (breaks){
            if (speed > 0.1f) speed -=0.2f;
            else if (speed < -0.1f) speed +=0.2f;
            else {
                speed=0;
                breaks = false;
            }
        }
        
        // 속도 text
        sss = (int)(speed * 10);
        speedT.text = sss.ToString();

        //1초 내에 속도변화가 일어나지 않으면 0.1초에 1씩 줄어듦 //귀찮아서 2초로 늘림
        if (speed != 0 && Time.time - timecheck >= 0.2) {
            timecheck = Time.time;
            if (speed < 0.1f && speed > -0.1f) speed = 0;
            else if (speed > 0) speed-=0.1f;
            else if (speed < 0) speed+=0.1f;
        }

        //마우스오버시 수행내용
        if (mouse.speedChange && !breaks)
        {
            changeSpeed();
        }
        else {
            velocity = 0;
        }

    }

    void changeSpeed(){ //속도변화

        if (mouse.crossP > 0) {
            velocity += 0.01f * mouse.crossP;
        }
        else if (mouse.crossP < 0){
            velocity += 0.01f * mouse.crossP;
        }

        if (velocity >= 1) {
            speed += accel;
            velocity = 0;
            timecheck = Time.time;
        }
        else if (velocity <= -1){
            speed -= accel;
            velocity = 0;
            timecheck = Time.time;
        }
    }
    void OnTriggerStay(Collider col) 
        {
            if(col.gameObject.tag == "BlackIce")
            {
                icecheck = false; //얼음에 닿았을 때 차체 회전 실행 안됨
            }
            
            else{
                icecheck = true;
            }
        }
    void OnTriggerEnter(Collider col){
        if (col.GetComponent<Collider>().CompareTag("gravel")){
            accel = 0.015f;
        }

        if (col.GetComponent<Collider>().CompareTag("gameOver")){
            print("게임오버1111111111111111111111111111111111111111111111111111111111111111111111");
            // 게임오버 엔딩창
        }
    }

    void OnTriggerExit(Collider col){
        
        if (col.GetComponent<Collider>().CompareTag("gravel")){
            accel = 0.01f;
        }
        
    }
}
