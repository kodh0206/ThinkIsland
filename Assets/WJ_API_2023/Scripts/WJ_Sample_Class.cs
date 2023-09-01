using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WjChallenge;
using TexDrawLib;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.TerrainTools;

public class WJ_Sample_Class : MonoBehaviour
{
    [SerializeField] WJ_Connector       wj_conn;
    [SerializeField] CurrentStatus      currentStatus;//진단평가 통과 여부
    public CurrentStatus                CurrentStatus => currentStatus;

    [Header("Panels")]
    [SerializeField] GameObject AnimationPanel;
    [SerializeField] GameObject panel_diag_chooseDiff;  //���̵� ���� �г�
    [SerializeField] GameObject         panel_question;         //���� �г�(����,�н�)
    [SerializeField] GameObject RadioPanel;

    [SerializeField] TEXDraw   textDescription;        //���� ���� �ؽ�Ʈ
    [SerializeField] TEXDraw   textEquation;           //���� �ؽ�Ʈ(��TextDraw�� ���� �ʿ�)
    [SerializeField] Button[]   btAnsr = new Button[4]; //���� ��ư��
    [SerializeField] Button quitButton; //홈메뉴 가는 버튼 
    [SerializeField] Button solving;
    TEXDraw[] textAnsr;                  //���� ��ư�� �ؽ�Ʈ(��TextDraw�� ���� �ʿ�)
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI energyText;
    [Header("Status")]
    int     currentQuestionIndex;
    bool    isSolvingQuestion;
    float   questionSolveTime;
    int currentLevel;
    int rightanswers; //맞춘 레밸 
    int energy; //획득한 에너지
    int gold; // 획득한 골드 
    private int wrongAnswerCount = 0;

    [Header("For Debug")]
    [SerializeField] WJ_DisplayText     wj_displayText;         //�ؽ�Ʈ ǥ�ÿ�(�ʼ�X)
    [SerializeField] Button             getLearningButton;      //���� �޾ƿ��� ��ư
    [SerializeField] GameObject         resultPanel;
    [SerializeField] GameObject         diagnosisPanel;
    [SerializeField] GameObject         warningPanel;

    //(20+ 맞춘갯수*(에너지총량-20)/8)
    private void Awake()
    {   
        textAnsr = new TEXDraw[btAnsr.Length];
        for (int i = 0; i < btAnsr.Length; ++i)

            textAnsr[i] = btAnsr[i].GetComponentInChildren<TEXDraw>();

        //wj_displayText.SetState("�����", "", "", "");
        //PlayerPrefs.DeleteAll(); //resetprefs

        string savedToken = PlayerPrefs.GetString("strAuthorization",string.Empty);
        if (!string.IsNullOrEmpty(savedToken))
        {
            currentStatus = CurrentStatus.LEARNING;
            //getLearningButton.interactable = true;
           
        }

        quitButton.onClick.AddListener(GoBackToMainMenu);
       Setup();
    }

    private void OnEnable()
    {   Debug.Log("현재상태"+currentStatus);
        Setup();
    }

    private void Setup()
    {    
        switch (currentStatus)
        {
            case CurrentStatus.WAITING:
                panel_diag_chooseDiff.SetActive(true);
                break;
            case CurrentStatus.LEARNING:
                panel_question.SetActive(true);
                break;

        }

        if (wj_conn != null)
        {
            wj_conn.onGetDiagnosis.AddListener(() => GetDiagnosis());
            wj_conn.onGetLearning.AddListener(() => GetLearning(0));

            
        }
        else Debug.LogError("Cannot find Connector");
    }

    private void Update()
    {
        if (isSolvingQuestion) questionSolveTime += Time.deltaTime;
        Debug.Log("현재 회원 :"+ wj_conn.strMBR_ID);
        
        Debug.Log("토큰 여부"+wj_conn.strAuthorization);
       
    }

    /// <summary>
    /// ������ ���� �޾ƿ���
    /// </summary>
    private void GetDiagnosis()
    {
        switch (wj_conn.cDiagnotics.data.prgsCd)
        {
            case "W"://학습 미완료
                MakeQuestion(wj_conn.cDiagnotics.data.textCn, 
                            wj_conn.cDiagnotics.data.qstCn, 
                            wj_conn.cDiagnotics.data.qstCransr, 
                            wj_conn.cDiagnotics.data.qstWransr);
                //wj_displayText.SetState("진단평가중", "", "", "");

                
                break;
            case "E":
                Debug.Log("진단평가 완료! �н� �ܰ�� �Ѿ�ϴ�.");
                //wj_displayText.SetState("", "", "", "");
                currentStatus = CurrentStatus.LEARNING;
                Debug.Log("진단 통과여부"+wj_conn.cDiagnotics.data.prgsCd);
                Debug.Log("진단후 token?"+wj_conn.strAuthorization);
                PlayerPrefs.SetString("strAuthorization", wj_conn.strAuthorization);
                PlayerPrefs.Save();
                diagnosisPanel.SetActive(true);
                GameController.Instance.currentActionPoints+=20;
                GameController.Instance.curentgold+=100;
                //getLearningButton.interactable = true;
                break;
        }
    }

    /// <summary>
    ///  n ��° �н� ���� �޾ƿ���
    /// </summary>
    private void GetLearning(int _index)
    {
        if (_index == 0) currentQuestionIndex = 0;

        MakeQuestion(
                    wj_conn.cLearnSet.data.qsts[_index].textCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCransr,
                    wj_conn.cLearnSet.data.qsts[_index].qstWransr);
    }

    /// <summary>
    /// �޾ƿ� �����͸� ������ ������ ǥ��
    /// </summary>
    private void MakeQuestion(string textCn, string qstCn, string qstCransr, string qstWransr)
    {   Debug.Log("질문 만드는중");
        panel_diag_chooseDiff.SetActive(false);
        panel_question.SetActive(true);

        string      correctAnswer;
        string[]    wrongAnswers;

        textDescription.text = textCn;
        textEquation.text = qstCn;

        correctAnswer = qstCransr;
        wrongAnswers    = qstWransr.Split(',');

        int ansrCount = Mathf.Clamp(wrongAnswers.Length, 0, 3) + 1;

        for(int i=0; i<btAnsr.Length; i++)
        {
            if (i < ansrCount)
                btAnsr[i].gameObject.SetActive(true);
            else
                btAnsr[i].gameObject.SetActive(false);
        }

        int ansrIndex = Random.Range(0, ansrCount);

        for(int i = 0, q = 0; i < ansrCount; ++i, ++q)
        {
            if (i == ansrIndex)
            {
                textAnsr[i].text = correctAnswer;
                --q;
            }
            else
                textAnsr[i].text = wrongAnswers[q];
        }
        isSolvingQuestion = true;
    }

    /// <summary>
    /// ���� ������ �¾Ҵ� �� üũ
    /// </summary>
    public void SelectAnswer(int _idx)
    {   
        bool isCorrect;
        string ansrCwYn = "N";

        switch (currentStatus)
        {
            case CurrentStatus.DIAGNOSIS:
                isCorrect   = textAnsr[_idx].text.CompareTo(wj_conn.cDiagnotics.data.qstCransr) == 0 ? true : false;
                ansrCwYn    = isCorrect ? "Y" : "N";

                if(isCorrect) 
                    {
                        // 정답일 때의 로직
                        Debug.Log("정답입니다!");
                        // 여기에 원하는 로직 추가 애니메이션 사운드 효과
                        AudioManager.Instance.PlayRight();
                    }
                else 
                    {
                        // 오답일 때의 로직
                        Debug.Log("틀렸습니다!");
                        AudioManager.Instance.PlayWrong();
                        // 여기에 원하는 로직 추가
                    }

                isSolvingQuestion = false;

                wj_conn.Diagnosis_SelectAnswer(textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                //wj_displayText.SetState("Time", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " ��");

                panel_question.SetActive(false);
                questionSolveTime = 0;

                APIanimationController.instance.ChangeAnimation(); // ChangeAnimationCode

                break;

            case CurrentStatus.LEARNING:
                Debug.Log("답 누르는중");
                isCorrect   = textAnsr[_idx].text.CompareTo(wj_conn.cLearnSet.data.qsts[currentQuestionIndex].qstCransr) == 0 ? true : false;
                ansrCwYn    = isCorrect ? "Y" : "N";
                if(isCorrect) 
                    {
                        // 정답일 때의 로직
                        Debug.Log("정답입니다!"); 
                        rightanswers+=1;
                        wrongAnswerCount = 0;
                        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
                        if (achievementManager != null)
                        {
                            achievementManager.IncrementAchievement("27", 1);
                        }

                        // 여기에 원하는 로직 추가 애니메이션 사운드 효과
                        AudioManager.Instance.PlayRight();
                    }
                else 
                    {
                        // 오답일 때의 로직
                        Debug.Log("틀렸습니다!");
                        // 여기에 원하는 로직 추가
                         wrongAnswerCount++; 
                        AudioManager.Instance.PlayWrong();
                    }
                   if (wrongAnswerCount >= 3)
                    {   warningPanel.SetActive(true);
                    for(int i=0; i<4; i++){
                        btAnsr[i].interactable =false;
                    }
                        Debug.Log("3문제 연속 틀렸습니다! 제대로 풀어주세요.");
                        wrongAnswerCount = 0;  // 경고를 주고 틀린 횟수 초기화
                    }

                // 문제 푸는 시간이 2초 이하인지 체크
                if (questionSolveTime <= 2.0f)
                {   for(int i=0; i<4; i++){
                        btAnsr[i].interactable =false;
                    }
                    warningPanel.SetActive(true);
                    Debug.Log("너무 빨리 풀었습니다! 제대로 풀어주세요.");
                }

                isSolvingQuestion = false;
                currentQuestionIndex++;

                wj_conn.Learning_SelectAnswer(currentQuestionIndex, textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                //wj_displayText.SetState("����Ǯ�� ��", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " ��");

                if (currentQuestionIndex >= 8) 
                {   
                    AudioManager.Instance.PlayFinished();
                    energy = 20+ 20 + rightanswers * (100- 20) / 8; //(GameController.Instance.maximumActionPoints- 20) / 8;
                    gold = 200+ (rightanswers*10);
                    energyText.text =energy.ToString();
                    goldText.text = gold.ToString();
                    
                    int energyToAdd = Math.Min(energy, 100 - GameController.Instance.currentActionPoints);
                    GameController.Instance.currentActionPoints += energyToAdd;
                    GameController.Instance.AddGold(gold);
                    resultPanel.SetActive(true);
                    //wj_displayText.SetState("����Ǯ�� �Ϸ�", "", "", "");
                    getLearningButton.interactable = true;

                    // 8문제를 모두 맞춘 경우
                    AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
                    if (rightanswers == 8)
                    {
                        if (achievementManager != null)
                        {
                            achievementManager.IncrementAchievement("26", 8);
                        }
                    }
                }
                else GetLearning(currentQuestionIndex);

                APIanimationController.instance.ChangeAnimation(); // ChangeAnimationCode

                questionSolveTime = 0;
                break;
        }
        
    }
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("BetaScene");
    }
    public void DisplayCurrentState(string state, string myAnswer, string isCorrect, string svTime)
    {
        if (wj_displayText == null) return;

        wj_displayText.SetState(state, myAnswer, isCorrect, svTime);
    }

    #region Unity ButtonEvent
    public void ButtonEvent_ChooseDifficulty(int a)
    {
        currentStatus = CurrentStatus.DIAGNOSIS;
        wj_conn.FirstRun_Diagnosis(a);
        //Level.text ="LV"+a.ToString();
    }
    public void ButtonEvent_GetLearning()
    {   rightanswers =0;
        gold =0;
        resultPanel.SetActive(false);
        panel_question.SetActive(true);
        wj_conn.Learning_GetQuestion();
        //wj_displayText.SetState("문제받기~", "-", "-", "-");
    }

    public void SetUpOn()
    {   
        solving.gameObject.SetActive(false);
        RadioPanel.SetActive(true);
        Setup();
        Debug.Log("문제 풀기 on");
    }

    public void CloseWarningPanel()
    {for(int i=0; i<4; i++){
                        btAnsr[i].interactable =true;
                    }
        warningPanel.SetActive(false);
    }
    #endregion
}