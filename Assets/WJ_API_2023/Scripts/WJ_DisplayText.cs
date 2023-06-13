using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WjChallenge;

public class WJ_DisplayText : MonoBehaviour
{
    [SerializeField] Text textCurrentState;

    string state        = "-";
    string myAnswer     = "-";
    string isCorrect    = "-";
    string svTime       = "-";

    void Start()
    {
        
    }

    /// <summary>
    /// ���� ����Ǯ�� ���¸� UI�� ǥ��
    /// </summary>
    /// <param name="state">���� ����</param>
    /// <param name="myAnswer">���� ���������� ���� ����</param>
    /// <param name="isCorrect">���������� ���� ���� ��������</param>
    /// <param name="svTime">Ǯ�̽ð�</param>
    public void SetState(string state, string myAnswer, string isCorrect, string svTime)
    {
        this.state      = state       != ""   ? state     : this.state ;
        this.myAnswer   = myAnswer    != ""   ? myAnswer  : this.myAnswer ;
        this.isCorrect  = isCorrect   != ""   ? isCorrect : this.isCorrect ;
        this.svTime     = svTime      != ""   ? svTime    : this.svTime ;

        textCurrentState.text =
            $"���� ���� : {this.state}\n" +
            $"�ֱ� ������ �� : {this.myAnswer}\n" +
            $"�ֱ� ���� ���� : {this.isCorrect}\n" +
            $"�ֱ� Ǯ�� �ð� : {this.svTime}\n";
    }
}
