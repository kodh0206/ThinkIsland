using UnityEngine;

public class Mg16Wall : MonoBehaviour
{
    public float wallBounceForce = 10f; // 벽에서 튕겨져 나가는 힘의 세기를 조절하기 위한 변수

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("실행");
        if (collision.gameObject.CompareTag("Wall")) // 충돌한 객체가 "Wall" 태그를 가지고 있는지 확인
        {
            Vector2 wallNormal = collision.contacts[0].normal; // 충돌 지점의 벽의 법선 벡터를 가져옴
            Vector2 bounceDirection = Vector2.Reflect(rb.velocity.normalized, wallNormal).normalized; // 현재 이동 방향을 벽의 법선에 대해 반사하여 튕기는 방향 계산
            rb.AddForce(bounceDirection * wallBounceForce, ForceMode2D.Impulse); // 튕기는 방향에 힘을 가해 플레이어를 튕겨냄
        }
    }
}
