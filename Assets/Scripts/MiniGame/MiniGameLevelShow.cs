using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MiniGameLevelShow : MonoBehaviour
{
    Animator changespriteAnimator;

    public SpriteRenderer spriteRenderer;
    public Sprite Level;
    public Sprite[] LevelSprite = new Sprite[12];
    public static MiniGameLevelShow Instance { get; private set; }
    
    public Image Changesprite;
    public Image BlackBoard;

    public float minAlpha = 0.3f;
    public float maxAlpha = 1f;


    private void Awake()
    {

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
        changespriteAnimator = Changesprite.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNowLevel(int difficulty ,bool changelevel)
    {
        if (changelevel)
        {
            switch (difficulty)
            {
                case 1: 
                    StartCoroutine(ChangeSprite0_1());
                    break;
                case 2:
                    StartCoroutine(ChangeSprite1_2());
                    break;
                case 3:
                    StartCoroutine(ChangeSprite2_3());
                    break;
            }

            StartCoroutine(ChangeSprite(difficulty));
        }
        else
        {
            switch (difficulty)
            {
                case 0:
                    StartCoroutine(ChangeSprite1_0());
                    break;
                case 1:
                    StartCoroutine(ChangeSprite2_1());
                    break;
                case 2:
                    StartCoroutine(ChangeSprite3_2());
                    break;
            }
        }


    }

    private IEnumerator ChangeSprite(int difficulty)
    {

        
        
        BlackBoard.enabled=true;
        Changesprite.enabled = true;
        
        Blink();
        yield return new WaitForSeconds(0.2f);
        BlinkEnd();
        changespriteAnimator.SetInteger("Level", difficulty);
        yield return new WaitForSeconds(0.5f);

        Changesprite.sprite = LevelSprite[difficulty];

        yield return new WaitForSeconds(0.2f);


        Changesprite.enabled = false;
        BlackBoard.enabled=false; 
    }


    public void Blink()
    {
        Changesprite.color = new Color(
                Changesprite.color.r,
                Changesprite.color.g,
                Changesprite.color.b,
                minAlpha);

    }

    public void BlinkEnd()
    {
        Changesprite.color = new Color(
            Changesprite.color.r,
            Changesprite.color.g,
            Changesprite.color.b,
            maxAlpha);
    }
    private IEnumerator ChangeSprite0_1()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[0];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[1];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[2];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[3];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }
    private IEnumerator ChangeSprite1_2()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[4];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[5];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[6];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[7];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }

    private IEnumerator ChangeSprite2_3()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[8];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[9];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[10];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[11];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }

    private IEnumerator ChangeSprite3_2()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[11];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[10];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[9];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[8];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }
    private IEnumerator ChangeSprite2_1()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[7];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[6];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[5];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[4];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }
    private IEnumerator ChangeSprite1_0()
    {
        BlackBoard.enabled = true;
        Changesprite.enabled = true;

        Changesprite.sprite = LevelSprite[3];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[2];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[1];
        yield return new WaitForSeconds(0.1f);
        Changesprite.sprite = LevelSprite[0];
        yield return new WaitForSeconds(0.1f);

        Changesprite.enabled = false;
        BlackBoard.enabled = false;
    }
}
