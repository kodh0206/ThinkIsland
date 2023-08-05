using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update    
    public TextMeshProUGUI dialogueText;
    public GameObject Textbox;
    public Animator teemoMovement;
    public SpriteRenderer teemo;
    private Queue<string> sentences;

    private void Start()
    {
        sentences =new Queue<string>();
        
        Invoke("Appearance",2);
        
    }
  
    public void Appearance()
    {
        teemoMovement.SetBool("isActive",true);
        Debug.Log("Appeared");
    }
    public void StartDialogue(Dialogue dialogue)
    {   Textbox.gameObject.SetActive(true);
        teemoMovement.SetBool("isTalking",true);
        sentences.Clear();

    string[] sentencesToEnqueue = new string[0];
    if (GameController.Instance.current_language == "English")
    {
        sentencesToEnqueue = dialogue.sentencesEnglish; 
    }
    else if (GameController.Instance.current_language == "Korean")
    {
        sentencesToEnqueue = dialogue.sentencesKorean;
    }
    // 추가 언어에 대한 처리...

    foreach (string sentence in sentencesToEnqueue)
    {
        sentences.Enqueue(sentence);
    }

    }
   
    //
    public void DisplayNextSentences()
    {

        if(sentences.Count ==0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {   Textbox.gameObject.SetActive(true);  
        teemoMovement.SetBool("isTalking",false);
        StartCoroutine(TeemoLeavingCoroutine());


    }

    IEnumerator TeemoLeavingCoroutine()
{
    // 2초 동안 대기
    yield return new WaitForSeconds(2f);
    
    // 대기 시간이 끝난 후 TeemoLeaving 메서드 실행
    TeemoLeaving();
}

    void TeemoLeaving()
    {
        teemoMovement.SetBool("isActive",false);
        dialogueText.gameObject.SetActive(false);
    }
}
