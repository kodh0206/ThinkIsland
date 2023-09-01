using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGameLevelShow : MonoBehaviour
{
    Animator changespriteAnimator;

    public SpriteRenderer spriteRenderer;
    public Sprite Level;
    public Sprite[] LevelSprite = new Sprite[4];
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

    public void ShowNowLevel(int difficulty)
    {

        StartCoroutine(ChangeSprite(difficulty));

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

}
