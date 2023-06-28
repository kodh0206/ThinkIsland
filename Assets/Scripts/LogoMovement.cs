using UnityEngine;

public class LogoMovement : MonoBehaviour
{
    public Sprite[] animationFrames; // 4컷 이미지를 저장할 배열
    public float frameRate = 0.2f; // 프레임 속도 조절

    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex = 0;
    private float timer = 0f;
    private bool isPlaying = false; // 현재 재생 중인지 확인하는 플래그


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartPlay() // 이 메서드가 호출되면 플레이가 시작됩니다.
    {
        isPlaying = true;
    }

    void Update()
    {
        timer += Time.deltaTime; // 경과 시간 측정

        if (timer >= frameRate) // 프레임 속도에 도달하면
        {
            timer = 0f; // 타이머 초기화

            if (!isPlaying && currentFrameIndex < 15) // 아직 16번째 프레임에 도달하지 않았다면
            {
                currentFrameIndex++; // 다음 프레임으로 이동
                spriteRenderer.sprite = animationFrames[currentFrameIndex]; // 현재 프레임을 스프라이트로 설정
            }

            if (isPlaying && currentFrameIndex >= 15 && currentFrameIndex < 23) // 플레이 중이고 아직 23번째 프레임에 도달하지 않았다면
            {
                currentFrameIndex++; // 다음 프레임으로 이동
                spriteRenderer.sprite = animationFrames[currentFrameIndex]; // 현재 프레임을 스프라이트로 설정
            }
        }  
    }
    
}