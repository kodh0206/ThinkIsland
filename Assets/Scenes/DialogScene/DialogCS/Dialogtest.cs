using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogtest : MonoBehaviour
{
    [SerializeField]
    private DialogSystem dialogSystem01;
    [SerializeField]
    private TextMeshProUGUI textCountdown;
    [SerializeField]
    private DialogSystem dialogSystem02;

    bool Korean = true;

    private IEnumerator Start()
    {
        //textCountdown.gameObject.SetActive(false);
        
        Korean = GameController.Instance.isKorean;
        // ù ��° ��� �б� ����
        if (Korean)
        {
            yield return new WaitUntil(() => dialogSystem01.UpdateDialog());
        }
        else
        {
            yield return new WaitUntil(() => dialogSystem02.UpdateDialog());
        }

        //// ��� �б� ���̿� ���ϴ� �ൿ�� �߰��� �� �ִ�.
        //// ĳ���͸� �����̰ų� �������� ȹ���ϴ� ����.. ����� 5-4-3-2-1 ī��Ʈ �ٿ� ����
        //textCountdown.gameObject.SetActive(true);
        //int count = 5;
        //while (count > 0)
        //{
        //    textCountdown.text = count.ToString();
        //    count--;

        //    yield return new WaitForSeconds(1);
        //}
        //textCountdown.gameObject.SetActive(false);

        //// �� ��° ��� �б� ����
        //yield return new WaitUntil(() => dialogSystem02.UpdateDialog());

        //textCountdown.gameObject.SetActive(true);
        //textCountdown.text = "The End";

        yield return new WaitForSeconds(2);
        Debug.Log("Dialog End");
        SceneManager.LoadScene("Main");
        //���� �� ��ȯ SceneChange

    }
}
