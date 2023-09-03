using UnityEngine;

public class GetJellyFxMovement : MonoBehaviour
{
    public Sprite[] animationFrames; // 4컷 이미지를 저장할 배열
    public float frameRate = 0.2f; // 프레임 속도 조절

    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex = 0;
    private float timer = 0f;
    private bool isAnimationDone = false; // 애니메이션이 끝났는지 여부를 확인하는 플래그

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAnimationDone) // 애니메이션이 끝나면 Update에서 더 이상 로직을 실행하지 않음
            return;

        timer += Time.deltaTime; // 경과 시간 측정

        if (timer >= frameRate) // 프레임 속도에 도달하면
        {
            timer = 0f; // 타이머 초기화
            currentFrameIndex++; // 다음 프레임으로 이동

            if (currentFrameIndex >= animationFrames.Length) // 마지막 프레임을 지나면
            {
                currentFrameIndex = animationFrames.Length - 1; // 마지막 프레임으로 설정
                isAnimationDone = true; // 애니메이션이 끝났음을 표시
            }

            spriteRenderer.sprite = animationFrames[currentFrameIndex]; // 현재 프레임을 스프라이트로 설정
        }
    }
}