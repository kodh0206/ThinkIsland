namespace WjChallenge
{
    #region ������ Ŭ���� (Diagnostic Class)
    using System.Collections.Generic;
    /// <summary>
    /// 게임 진단 문항 요청
    /// </summary>
    public class Request_DN_Setting
    {
        public string gameCd;      //게임번호
        public string mbrId;        //맴버아이디
        public string deviceNm;     //디바이스 이름
        public string gameVer;      //게임버전
        public string osScnCd;      //OS 구분
        public string langCd;       //학습 언어코드
        public int timeZone;        //한국 +9
        public string bgnLvl;       //시작 수준(A,B,C,D) 필수아님
    }

    /// <summary>
    ///게임 진단 결과 전송
    /// </summary>
    public class Request_DN_Progress
    {
        public string gameCd;       //게임코드
        public string mbrId;        //맴버ID
        public string prgsCd;       //진행코드 진단진행 
        public long sid;            //진단 ID
        public string qstCd;        //문항코드
        public string qstCransr;    //문항정답
        public string ansrCwYn;     //정오답 여부-Y/N
        public long slvTime;        // 푸는데 걸린시간 

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
        public bool result;//결과 코드
        public string msg;//결과 메세지
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
        public long sid; //진단 ID
        public string prgsCd;       //진행코드 W:진단진행 E:진단완료
        public string qstCd;        //문항코드
        public string qstCn;        //문항내용
        public string textCn;       //지문내용
        public string qstCransr;    //문항 정답
        public string qstWransr;    //문항 오답
        public int accuracy;        //진단 정확도 수준
        public int estQstNowNo;     //현재까지 푼 문항수
        public string estPreStgCd;  //적정 시작 지점의 스테이지 코드
    }
    #endregion

    #region �н� Ŭ���� (Learning Class)
    /// <summary>
    /// 게임 학습 문항 요청 
    /// </summary>
    [System.Serializable]
    public class Request_Learning_Setting
    {
        public string gameCd;//게임코드
        public string mbrId;//회원ID
        public string gameVer;//게임버전
        public string osScnCd;//OS구분
        public string deviceNm;//디바이스 이름
        public string langCd;//언어코드
        public int timeZone;//시간대
        public string mathpidId;//매쓰 피드 ID
    }

    /// <summary>
    /// 학습 
    /// </summary>=
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
        public long sid;// 학습 진단코드
        public string bgnDt;//학습 시작 시간
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
    /// 게임 학습 결과 전송 
    /// </summary>
    [System.Serializable]
    public class Response_Learning_ProgressData
    {
        public string explSpedCd;//풀이속도 코드
        public int explSped;//풀이 속도
        public string lrnPrgsStsCd;//학습 진행 상태 코드
        public string acrcyCd;//풀이 정확도 콛,
        public int explAcrcyRt;//풀이 정확도률
    }

    /// <summary>
    /// ����Ǯ�� ��û �� �޾ƿ��� ���� ����
    /// </summary>
    [System.Serializable]
    public class Learning_Question
    {
        public string qstCd;//문항코드
        public string qstCn;//문항 내용
        public string textCn;//지문 내용
        public string qstCransr;//문항정답
        public string qstWransr;//문항오답
    }

    /// <summary>
    /// ����Ǯ�� �Ϸ� �� ������ ���� ���
    /// </summary>
    [System.Serializable]
    public class Learning_MyAnsr
    {
        public string qstCd; //문항코드
        public string qstCransr;//문항정답
        public string ansrCwYn;//정오답 여우
        public long slvTime;//문제 풀이 시간 

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
    





