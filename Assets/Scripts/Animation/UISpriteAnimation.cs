using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    bool IsDone;

    public void Func_PlayUIAnim()
    {
        // 이미 재생 중이면 애니메이션을 중지하고 초기화
        if (!IsDone)
        {
            Func_StopUIAnim();
            Func_ResetUIAnim();
        }

        IsDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        IsDone = true;
        StopAllCoroutines(); // 모든 코루틴 중지
    }

    IEnumerator Func_PlayAnimUI()
    {
        while (!IsDone)
        {
            yield return new WaitForSeconds(m_Speed);

            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                IsDone = true; // 애니메이션이 끝났으므로 플래그를 설정
                Func_ResetUIAnim();
                break; // 루프 종료
            }

            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;
        }
    }

    
    public void Func_ResetUIAnim()
    {
        m_IndexSprite = 0; // 인덱스를 초기화
        m_Image.sprite = m_SpriteArray[m_IndexSprite]; // 첫 번째 스프라이트로 설정
    }
}