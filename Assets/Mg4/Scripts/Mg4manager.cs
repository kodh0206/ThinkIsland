using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg4manager : MonoBehaviour
{
    public static Mg4manager instance = null;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;
    private int score = 0; // score=jelly ���ھ ���� ������ �������� ������ �ӵ� ��������
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
    score += 1;
    MiniGameManager.Instance.AddJelly();  // 젤리 추가 및 UI 업데이트
    scoreText.text = "Eat jelly " + score;

    if (score % 5 == 0)
    {
        makejelly spawner = FindObjectOfType<makejelly>();
        Makebirdpoop spawner2 = FindObjectOfType<Makebirdpoop>();
        jelly jell = FindObjectOfType<jelly>();
        birdpoop poop = FindObjectOfType<birdpoop>();
        
        if (spawner != null)
        {
            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();
        }
    }

}
}