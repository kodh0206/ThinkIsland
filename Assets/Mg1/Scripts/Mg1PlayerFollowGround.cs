using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg1PlayerFollowGround : MonoBehaviour
{
    Mg1Player mg1Player;
    private GameObject ground;
    private Vector3 groundPosition;
    private Vector3 distance; // 바닥 위 아래
    public bool isRightButtonPressed = false; // Right 버튼이 눌렸는지 여부
    public bool isJumpButtonPressed = false; // Jump 버튼이 눌렸는지 여부
    public Vector3 currentPosition; // 플레이어의 현재 위치
    
    private void Start()
    {
        mg1Player = GetComponent<Mg1Player>();
    }

    void Update()
    {
        // 움직이는 발판에서 같이 움직이기
        if (ground != null)
        {
            // 버튼이 눌리지 않은 경우
            if (!mg1Player.isPlayerReset && mg1Player.isGrounded && !isRightButtonPressed && !isJumpButtonPressed && mg1Player.rightButton)
            {
                // 캐릭터의 위치 = 밟고 있는 플랫폼과 distance 만큼 떨어진 위치
                transform.position = ground.transform.position - distance;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 접촉한 오브젝트의 태그가 Ground 일 때,
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 접촉한 땅 지정
            ground = collision.gameObject;

            // 접촉한 순간의 오브젝트 위치를 저장
            groundPosition = ground.transform.position; // 땅 위치 저장
            currentPosition = transform.position; // 캐릭터 위치 저장

            // 접촉한 순간의 오브젝트 위치와 캐릭터 위치의 차이를 distance에 저장
            distance = groundPosition - currentPosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 접촉한 오브젝트의 태그가 Ground 일 때,
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 접촉한 땅 지정
            ground = collision.gameObject;

            // 접촉한 순간의 오브젝트 위치를 저장
            groundPosition = ground.transform.position; // 땅 위치 저장
            currentPosition = transform.position; // 캐릭터 위치 저장

            // 접촉한 순간의 오브젝트 위치와 캐릭터 위치의 차이를 distance에 저장
            distance = groundPosition - currentPosition;
        }
    }
}
