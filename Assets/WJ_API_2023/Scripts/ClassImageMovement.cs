using UnityEngine;

public class ClassImageMovement : MonoBehaviour
{
    public Sprite[] animationFrames; // 4컷 이미지를 저장할 배열
    public float frameRate = 0.2f; // 프레임 속도 조절

    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime; // 경과 시간 측정

        if (timer >= frameRate) // 프레임 속도에 도달하면
        {
            timer = 0f; // 타이머 초기화
            currentFrameIndex++; // 다음 프레임으로 이동

            if (currentFrameIndex >= animationFrames.Length) // 마지막 프레임을 지나면 처음 프레임으로 돌아감
                currentFrameIndex = 0;

            spriteRenderer.sprite = animationFrames[currentFrameIndex]; // 현재 프레임을 스프라이트로 설정
        }
    }
}