using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameLevelShow : MonoBehaviour
{


    public Sprite Level;
    public Sprite[] LevelSprite = new Sprite[4];
    public static MiniGameLevelShow Instance { get; private set; }
    
    public Image Changesprite;
    public Image BlackBoard;

    // Ŭ������ �ٸ� ��� ������ �Լ� ��...

    private void Awake()
    {
        // Instance ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        BlackBoard.enabled=false;
        Changesprite.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNowLevel(int difficulty)
    {
        Debug.Log("������������Ʈ�������2");
        StartCoroutine(ChangeSprite(difficulty));
        Debug.Log("������������Ʈ���泡");
    }

    private IEnumerator ChangeSprite(int difficulty)
    {
        Debug.Log("������������Ʈ�������3");
        
        Changesprite.sprite = LevelSprite[difficulty];
        BlackBoard.enabled=true;
        Changesprite.enabled = true; // �̹����� Ȱ��ȭ�Ͽ� ���̵��� ��

        yield return new WaitForSeconds(1f);

        Changesprite.enabled = false;
        BlackBoard.enabled=false; // �̹����� ��Ȱ��ȭ�Ͽ� ����
    }

}