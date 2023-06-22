namespace WjChallenge
{
    #region ������ Ŭ���� (Diagnostic Class)
    using System.Collections.Generic;
    /// <summary>
    /// ���� ���� ���׿�û�� ������ ��(������ ù ���� ��)
    /// </summary>
    public class Request_DN_Setting
    {
        public string gameCd;      //게임번호
        public string mbrId;        //맴버아이디
        public string deviceNm;     //����̽� �̸�
        public string gameVer;      //���� ����
        public string osScnCd;      //OS ����
        public string langCd;       //�н� ����ڵ�
        public int timeZone;        //�ѱ� +9
        public string bgnLvl;       //���� ����(A,B,C,D)
    }

    /// <summary>
    /// ���� ���� ����Ǯ�̽� ������ ��(������ ���� ��)
    /// </summary>
    public class Request_DN_Progress
    {
        public string gameCd;       //�����ڵ�
        public string mbrId;        //ȸ��ID
        public string prgsCd;       //�����ڵ�(W : ��������, E : ���ܿϷ�, X : ��Ÿ���)
        public long sid;            //����ID
        public string qstCd;        //Ǭ ������ ���� �ڵ�
        public string qstCransr;    //�Է��� �� ����
        public string ansrCwYn;     //������ ����-Y/N
        public long slvTime;        //���� Ǯ�� �ð�(ms)

        public Request_DN_Progress()
        {

        }
    }

    /// <summary>
    /// ������ �� �޾ƿ��� ��(������ ù����, ������ ����)
    /// </summary>
    [System.Serializable]
    public class DN_Response
    {
        public bool result;
        public string msg;
        public Diagnotics_Data data;

        public DN_Response()
        {
            data = new Diagnotics_Data();
        }
    }

    /// <summary>
    /// ������ ���� �� �޾ƿ��� ���� ������
    /// </summary>
    [System.Serializable]
    public class Diagnotics_Data
    {
        public long sid;
        public string prgsCd;       //���� �ڵ�(W : ��������, E : ���ܿϷ�)
        public string qstCd;        //�����ڵ�
        public string qstCn;        //���׳���
        public string textCn;       //��������
        public string qstCransr;    //��������
        public string qstWransr;    //���׿���
        public int accuracy;        //���� ��Ȯ�� ����
        public int estQstNowNo;     //������� ���� ��(���� ���� ����)
        public string estPreStgCd;  //���� ���� ������ �������� �ڵ�
    }

    #endregion

    #region �н� Ŭ���� (Learning Class)
    /// <summary>
    /// ����Ǯ��(�н�) �� ���׿�û�� ������ ��
    /// </summary>
    [System.Serializable]
    public class Request_Learning_Setting
    {
        public string gameCd;
        public string mbrId;
        public string gameVer;
        public string osScnCd;
        public string deviceNm;
        public string langCd;
        public int timeZone;
        public string mathpidId;
    }

    /// <summary>
    /// ����Ǯ��(�н�) ��û �� �޾ƿ��� ��
    /// </summary>
    [System.Serializable]
    public class Response_Learning_Setting
    {
        public bool result;
        public string msg;
        public Response_Learning_SettingData data;
    }

    [System.Serializable]
    public class Response_Learning_SettingData
    {
        public long sid;
        public string bgnDt;
        public List<Learning_Question> qsts;
    }

    /// <summary>
    /// ����Ǯ��(�н�) �Ϸ� �� ������ ��
    /// </summary>
    public class Request_Learning_Progress
    {
        public string gameCd;
        public string mbrId;
        public string prgsCd;
        public long sid;
        public string bgnDt;

        public List<Learning_MyAnsr> data;

        public Request_Learning_Progress()
        {
            data = new List<Learning_MyAnsr>();
        }
    }
    [System.Serializable]
    public class Response_Learning_Progress
    {
        public bool result;
        public string msg;
        public Response_Learning_ProgressData data;
    }

    /// <summary>
    /// ����Ǯ�� ���
    /// </summary>
    [System.Serializable]
    public class Response_Learning_ProgressData
    {
        public string explSpedCd;
        public int explSped;
        public string lrnPrgsStsCd;
        public string acrcyCd;
        public int explAcrcyRt;
    }

    /// <summary>
    /// ����Ǯ�� ��û �� �޾ƿ��� ���� ����
    /// </summary>
    [System.Serializable]
    public class Learning_Question
    {
        public string qstCd;
        public string qstCn;
        public string textCn;
        public string qstCransr;
        public string qstWransr;
    }

    /// <summary>
    /// ����Ǯ�� �Ϸ� �� ������ ���� ���
    /// </summary>
    [System.Serializable]
    public class Learning_MyAnsr
    {
        public string qstCd;
        public string qstCransr;
        public string ansrCwYn;
        public long slvTime;

        public Learning_MyAnsr(string _cd, string _cransr, string _cwyn, long _time)
        {
            qstCd = _cd;
            qstCransr = _cransr;
            ansrCwYn = _cwyn;
            slvTime = _time;
        }
    }
    #endregion
}
