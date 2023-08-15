using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WjChallenge;
using TexDrawLib;
using UnityEngine.SceneManagement;


public class WJ_Sample_Class : MonoBehaviour
{
    [SerializeField] WJ_Connector       wj_conn;
    [SerializeField] CurrentStatus      currentStatus;//진단평가 통과 여부
    public CurrentStatus                CurrentStatus => currentStatus;

    [Header("Panels")]
    [SerializeField] GameObject AnimationPanel;
    [SerializeField] GameObject         panel_diag_chooseDiff;  //���̵� ���� �г�
    [SerializeField] GameObject         panel_question;         //���� �г�(����,�н�)
    [SerializeField] GameObject RadioPanel;

    [SerializeField] TEXDraw   textDescription;        //���� ���� �ؽ�Ʈ
    [SerializeField] TEXDraw   textEquation;           //���� �ؽ�Ʈ(��TextDraw�� ���� �ʿ�)
    [SerializeField] Button[]      btAnsr = new Button[4]; //���� ��ư��
    [SerializeField] Button quitButton; //홈메뉴 가는 버튼 
    [SerializeField] Button solving;
    TEXDraw[] textAnsr;                  //���� ��ư�� �ؽ�Ʈ(��TextDraw�� ���� �ʿ�)

    [Header("Status")]
    int     currentQuestionIndex;
    bool    isSolvingQuestion;
    float   questionSolveTime;
    int currentLevel;
    [Header("For Debug")]
    [SerializeField] WJ_DisplayText     wj_displayText;         //�ؽ�Ʈ ǥ�ÿ�(�ʼ�X)
    [SerializeField] Button             getLearningButton;      //���� �޾ƿ��� ��ư

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
                wj_displayText.SetState("진단평가중", "", "", "");

                
                break;
            case "E":
                Debug.Log("진단평가 완료! �н� �ܰ�� �Ѿ�ϴ�.");
                wj_displayText.SetState("", "", "", "");
                currentStatus = CurrentStatus.LEARNING;
                Debug.Log("진단 통과여부"+wj_conn.cDiagnotics.data.prgsCd);
                Debug.Log("진단후 token?"+wj_conn.strAuthorization);
                PlayerPrefs.SetString("strAuthorization", wj_conn.strAuthorization);
                PlayerPrefs.Save();
                getLearningButton.interactable = true;
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

                isSolvingQuestion = false;

                wj_conn.Diagnosis_SelectAnswer(textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                wj_displayText.SetState("Time", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " ��");

                panel_question.SetActive(false);
                questionSolveTime = 0;

                APIanimationController.instance.ChangeAnimation(); // ChangeAnimationCode

                break;

            case CurrentStatus.LEARNING:
                Debug.Log("답 누르는중");
                isCorrect   = textAnsr[_idx].text.CompareTo(wj_conn.cLearnSet.data.qsts[currentQuestionIndex].qstCransr) == 0 ? true : false;
                ansrCwYn    = isCorrect ? "Y" : "N";

                isSolvingQuestion = false;
                currentQuestionIndex++;

                wj_conn.Learning_SelectAnswer(currentQuestionIndex, textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                //wj_displayText.SetState("����Ǯ�� ��", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " ��");

                if (currentQuestionIndex >= 8) 
                {
                    panel_question.SetActive(false);
                    wj_displayText.SetState("����Ǯ�� �Ϸ�", "", "", "");
                    getLearningButton.interactable = true;
                }
                else GetLearning(currentQuestionIndex);

                APIanimationController.instance.ChangeAnimation(); // ChangeAnimationCode

                questionSolveTime = 0;
                break;
        }
        
    }
    void GoBackToMainMenu()
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
    {
        wj_conn.Learning_GetQuestion();
        wj_displayText.SetState("문제받기~", "-", "-", "-");
    }

    public void SetUpOn()
    {   
        solving.gameObject.SetActive(false);
        RadioPanel.SetActive(true);
        Setup();
        Debug.Log("문제 풀기 on");
    }
    #endregion
}