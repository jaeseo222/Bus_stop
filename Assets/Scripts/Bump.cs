﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{
    private int speedBump; // 방지턱 위반 속도 4가지
    public GameObject obj30; // 표지판 30
    public GameObject obj40; // 표지판 40
    public GameObject obj50; // 표지판 50
    public GameObject obj60; // 표지판 60
    public AudioClip audioBump;
    public GameObject bump; // 방지턱
    private int rand;

    AudioSource audioSource;
    public Rigidbody rd;

    void Start()
    {
        rand = Random.Range(3, 7); // 변수에 랜덤으로 3, 4, 5, 6 저장

        // 속도 지정 및 표지판 생성
        if (bump.CompareTag("SummerBump1"))
        {
            switch (rand)
            {
                case 3:
                    speedBump = 30;
                    Instantiate(obj30, new Vector3(37.67f, 0.453f, 37.01f), Quaternion.Euler(-90.0f, -90.0f, 0));
                    break;
                case 4:
                    speedBump = 40;
                    Instantiate(obj40, new Vector3(37.67f, 0.453f, 37.01f), Quaternion.Euler(-90.0f, -90.0f, 0));
                    break;
                case 5:
                    speedBump = 50;
                    Instantiate(obj50, new Vector3(37.67f, 0.453f, 37.01f), Quaternion.Euler(0, -90.0f, 0));
                    break;
                case 6:
                    speedBump = 60;
                    Instantiate(obj60, new Vector3(37.67f, 0.453f, 37.01f), Quaternion.Euler(-90.0f, -90.0f, 0));
                    break;
            }
        }
        else if(bump.CompareTag("SummerBump2"))
        {
            switch (rand)
            {
                case 3:
                    speedBump = 30;
                    Instantiate(obj30, new Vector3(-3.195f, 12.118f, 8.185f), Quaternion.Euler(-122.0f, 270.0f, -90.0f));
                    break;
                case 4:
                    speedBump = 40;
                    Instantiate(obj40, new Vector3(-3.195f, 12.118f, 8.185f), Quaternion.Euler(-122.0f, 270.0f, -90.0f));
                    break;
                case 5:
                    speedBump = 50;
                    Instantiate(obj50, new Vector3(-3.195f, 12.118f, 8.185f), Quaternion.Euler(0, -180.0f, 32.0f));
                    break;
                case 6:
                    speedBump = 60;
                    Instantiate(obj60, new Vector3(-3.195f, 12.118f, 8.185f), Quaternion.Euler(-122.0f, 270.0f, -90.0f));
                    break;
            }
        }
        else if(bump.CompareTag("SummerBump3"))
        {
            switch (rand)
            {
                case 3:
                    speedBump = 30;
                    Instantiate(obj30, new Vector3(-17.68f, 15.451f, 44.154f), Quaternion.Euler(-90.0f, 180.0f, 0));
                    break;
                case 4:
                    speedBump = 40;
                    Instantiate(obj40, new Vector3(-17.68f, 15.451f, 44.154f), Quaternion.Euler(-90.0f, 180.0f, 0));
                    break;
                case 5:
                    speedBump = 50;
                    Instantiate(obj50, new Vector3(-17.68f, 15.451f, 44.154f), Quaternion.Euler(0, 180.0f, 0));
                    break;
                case 6:
                    speedBump = 60;
                    Instantiate(obj60, new Vector3(-17.68f, 15.451f, 44.154f), Quaternion.Euler(-90.0f, 180.0f, 0));
                    break;
            }
        }
        else if(bump.CompareTag("SummerBump4"))
        {
            switch (rand)
            {
                case 3:
                    speedBump = 30;
                    Instantiate(obj30, new Vector3(8.8846f, 4.581f, -41.95f), Quaternion.Euler(-122.0f, -90.0f, 90.0f));
                    break;
                case 4:
                    speedBump = 40;
                    Instantiate(obj40, new Vector3(8.8846f, 4.581f, -41.95f), Quaternion.Euler(-122.0f, -90.0f, 90.0f));
                    break;
                case 5:
                    speedBump = 50;
                    Instantiate(obj50, new Vector3(8.8846f, 4.581f, -41.95f), Quaternion.Euler(0, 0, -32.0f));
                    break;
                case 6:
                    speedBump = 60;
                    Instantiate(obj60, new Vector3(8.8846f, 4.581f, -41.95f), Quaternion.Euler(-122.0f, -90.0f, 90.0f));
                    break;
            }
        }

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = audioBump;
        audioSource.volume = 1.0f;
        audioSource.mute = true;
    }

    // 충돌 감지
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bus")) // 충돌한 오브젝트의 태그가 Bus인지 검사
        {
            if (bus.sss > speedBump) // 랜덤으로 지정된 속도 이상일 때
            {
                // 충돌 효과음 내기
                audioSource.mute = false;
                audioSource.Play();
                // 버스 튀어오르는 모션
                rd.AddRelativeForce(new Vector3(1, 0, 0) * 200000);
                rd.AddRelativeForce(new Vector3(0, 1, 0) * 800000);
                rd.AddRelativeForce(new Vector3(0, 0, -1) * 500000);
            }
        }
    }
}