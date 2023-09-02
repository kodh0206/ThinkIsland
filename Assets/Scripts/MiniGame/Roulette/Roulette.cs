using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Roulette : MonoBehaviour
{
	[SerializeField]
	private	Transform			piecePrefab;				// �귿�� ǥ�õǴ� ���� ������
	[SerializeField]
	private	Transform			linePrefab;					// �������� �����ϴ� �� ������
	[SerializeField]
	private	Transform			pieceParent;				// �������� ��ġ�Ǵ� �θ� Transform
	[SerializeField]
	private	Transform			lineParent;					// ������ ��ġ�Ǵ� �θ� Transform
	[SerializeField]
	private	 List<RoulettePieceData> 	roulettePieceData;			// �귿�� ǥ�õǴ� ���� �迭

	[SerializeField]
	private	int					spinDuration;				// ȸ�� �ð�
	[SerializeField]
	private	Transform			spinningRoulette;			// ���� ȸ���ϴ� ȸ���� Transfrom
	[SerializeField]
	private	AnimationCurve		spinningCurve;				// ȸ�� �ӵ� ��� ���� �׷���

    public Button rewardButton;
    [SerializeField] UISpriteAnimation spriteAnimation;
    [SerializeField] GameObject rewardPanel;
	private	float				pieceAngle;					// ���� �ϳ��� ��ġ�Ǵ� ����
	private	float				halfPieceAngle;				// ���� �ϳ��� ��ġ�Ǵ� ������ ���� ũ��
	private	float				halfPieceAngleWithPaddings;	// ���� ���⸦ ������ Padding�� ���Ե� ���� ũ��
	
	private	int					accumulatedWeight;			// ����ġ ����� ���� ����
	private	bool				isSpinning = false;			// ���� ȸ��������
	private	int					selectedIndex = 0;			// �귿���� ���õ� ������

    public Color color1 = HexToColor("C49A6C"); // 첫 번째 색상 (빨강)
	public Color color2 = HexToColor("DCB98A"); // 두 번째 색상 (파랑)

	public Sprite gold;
    private int spinDursation=3;
    
    [SerializeField]
	private AudioClip rouletteSpinClip; // Add this line to declare AudioClip for roulette spinning sound

	private AudioSource audioSource; // Add this line to declare AudioSource
    public AudioClip spin;
    public AudioClip reward;
    private bool isAnimating = false;
    public TextMeshProUGUI jellyCounter;
    void Awake() // Or you can use Start() method
	{
		audioSource = GetComponent<AudioSource>();
		if(audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}
      void OnEnable()
    {
        // 씬 로드 이벤트에 대한 콜백을 등록합니다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 씬 로드 이벤트에 대한 콜백을 제거합니다.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {  
        jellyCounter.text =GameController.Instance.currentjellyCount.ToString();
       roulettePieceData = new List<RoulettePieceData>
	{
    new RoulettePieceData { icon = gold, description = "Gold 300", rewardType = "Gold", rewardAmount = 300, chance = 25 },
    new RoulettePieceData { icon = gold, description = "Gold 500", rewardType = "Gold", rewardAmount = 500, chance = 35 },
    new RoulettePieceData { icon = gold, description = "Gold 1000", rewardType = "Gold", rewardAmount = 1000, chance = 40 }
	};

    List<LevelRewardData> newRewards = RewardManager.Instance.GetNewRewards();

        // LevelRewardData를 RoulettePieceData로 변환
        List<RoulettePieceData> convertedRewards = RewardManager.Instance.ConvertLevelRewardsToPieces(newRewards);

        // 변환된 보상들을 룰렛에 추가
        foreach (RoulettePieceData reward in convertedRewards)
        {
            // 기존에 같은 보상이 없으면 추가
            if (!roulettePieceData.Exists(x => x.description == reward.description && x.rewardType == reward.rewardType))
            {
                roulettePieceData.Add(reward);
            }
            // 이미 같은 보상이 있으면 해당 보상의 확률을 업데이트
            else
            {
                RoulettePieceData existingReward = roulettePieceData.Find(x => x.description == reward.description && x.rewardType == reward.rewardType);
                existingReward.chance += reward.chance;
            }
        }
    	pieceAngle = 360 / roulettePieceData.Count;
        halfPieceAngle = pieceAngle * 0.5f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

        
    	SpawnPiecesAndLines();
    	CalculateWeightsAndIndices();
    }
	
	private void SpawnPiecesAndLines()
	{   
    
		bool useColor1 = true; // 번갈아 가면서 색상을 사용하기 위한 플래그
		
		for (int i = 0; i < roulettePieceData.Count; ++i)
		{
			Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
			piece.GetComponent<RoulettePiece>().Setup(roulettePieceData[i]);
			piece.GetComponent<RoulettePiece>().SetColor(useColor1 ? color1 : color2); // 색상 설정
			piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));

			Transform line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);
			line.RotateAround(lineParent.position, Vector3.back, (pieceAngle * i) + halfPieceAngle);
			
			useColor1 = !useColor1; // 색상 플래그 토글
		}
        
	}

	private void CalculateWeightsAndIndices()
	{
	accumulatedWeight = 0;
    for (int i = 0; i < roulettePieceData.Count; ++i)
    {
        roulettePieceData[i].index = i;
        accumulatedWeight += roulettePieceData[i].chance;
        roulettePieceData[i].weight = accumulatedWeight;
    }
	}

	private int GetRandomIndex()
	{
	  int randomValue = UnityEngine.Random.Range(0, accumulatedWeight + 1);

    for (int i = 0; i < roulettePieceData.Count; ++i)
    {
        if (randomValue <= roulettePieceData[i].weight)
        {
            return i;
        }
    }

    return 0;  // 이 부분은 필요에 따라 다른 값으로 변경할 수 있습니다.

	}
 public void Spin(UnityAction<RoulettePieceData> action=null)
    {  
    if (GameController.Instance.currentjellyCount >= 10)
    {
        // 이미 회전 중이라면 return
        if (isSpinning == true) return;

        // 룰렛의 결과 값 선택
        selectedIndex = GetRandomIndex();

        // 선택된 결과의 중심 각도
        float angle = pieceAngle * selectedIndex;

        // 정확히 중심이 아닌 결과 값 범위 안의 임의의 각도 선택
        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360;
        float randomAngle = Random.Range(leftOffset, rightOffset);

        // 목표 각도(targetAngle) = 결과 각도 + 360 * 회전 시간 * 회전 속도
        int rotateSpeed = 2;
        float targetAngle = (randomAngle + 360 * spinDuration * rotateSpeed);

        isSpinning = true;

        // 젤리 10개 지불
        GameController.Instance.currentjellyCount -= 10;
        jellyCounter.text =GameController.Instance.currentjellyCount.ToString();
        StartCoroutine(OnSpin(targetAngle, action));
    }
    else
    {   
        
        // 젤리가 부족하므로 메인 메뉴로 돌아갑니다.
        Debug.Log("Not enough jelly to spin the wheel. Returning to main menu.");
        StartCoroutine(WaitAndLoadMainScene(0));  // 0초 대기 후 메인 메뉴로 돌아갑니다.
    }
    }
private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
{
  float current = 0;
    float percent = 0;
    
    audioSource.clip = spin;
    audioSource.Play();  // 룰렛이 회전하기 시작하면 소리를 재생합니다.

    while (percent < 1)
    {
        current += Time.deltaTime;
        percent = current / spinDuration;

        float z = Mathf.Lerp(0, end, spinningCurve.Evaluate(percent));
        spinningRoulette.rotation = Quaternion.Euler(0, 0, z);

        yield return null;
    }

    // 리스트가 비어 있는지 확인
    if (roulettePieceData.Count == 0)
    {
        Debug.LogError("roulettePieceData is empty.");
        yield break;  // 코루틴을 중단합니다.
    }

    Debug.Log("Spin completed. Selected index: " + selectedIndex); 
    isSpinning = false;

    if (action != null) action.Invoke(roulettePieceData[selectedIndex]);

    // 보상 처리 부분
    RoulettePieceData selectedReward = roulettePieceData[selectedIndex];
    Debug.Log("Selected reward: " + selectedReward.description); 

    // 아이템 제거 전에도 한 번 더 확인
    if (roulettePieceData.Count > 0)
    {
    if (roulettePieceData[selectedIndex].rewardType != "Gold")
    {
        roulettePieceData.RemoveAt(selectedIndex);
    }
    else
    {
    Debug.LogError("Cannot remove item. roulettePieceData is empty.");
    }
    }
    
    else
    {
        Debug.LogError("Cannot remove item. roulettePieceData is empty.");
    }

    // Find the reward in the newRewards list that matches the selected reward
    LevelRewardData rewardToRemove = RewardManager.Instance.GetMatchedNewReward(selectedReward.description);
    // If the reward is in the list, remove it
    if (rewardToRemove != null)
    {
        RewardManager.Instance.RemoveFromNewRewards(rewardToRemove);
    }
    
    spriteAnimation.m_SpriteArray[spriteAnimation.m_SpriteArray.Length-1] = selectedReward.icon;
    audioSource.Stop();

    // 보상 유형에 따라 다른 작업을 수행합니다.
    switch (selectedReward.rewardType)
    {
        case "Gold":
            // 골드 보상 처리
            // 예를 들어 플레이어의 골드를 증가시키는 코드 등
            GameController.Instance.curentgold += selectedReward.rewardAmount;
            break;
        case "Crop":
            CropData newCrop = GameController.Instance.CropList.Find(c => c.plantName == selectedReward.description);
            if (newCrop != null)
            {
                GameController.Instance.currentUnlockedCrops.Add(newCrop);
            }
            break;
        case "MiniGame":
            if (!string.IsNullOrEmpty(selectedReward.description))
            {
                // 만약 미니게임이 이미 해금된 상태라면 무시합니다.
                if (GameController.Instance.unlockedMiniGames.Contains(selectedReward.description))
                {
                    Debug.Log("This minigame is already unlocked.");
                    break;
                }

                // 새 미니게임을 해금 리스트에 추가합니다.
                GameController.Instance.unlockedMiniGames.Add(selectedReward.description);
                MiniGameManager.Instance.AddMiniGame(selectedReward.description);
                Debug.Log("Unlocked new minigame: " + selectedReward.description);
            }
            break;
        default:
            break;
    }
    UpdateRouletteWheel();

    yield return null;
     
}

private IEnumerator WaitAndLoadMainScene(float waitTime)
{
    yield return new WaitForSeconds(waitTime);
    MiniGameManager.Instance.LoadMainMenu();
}

void AddPiece(RoulettePieceData newPiece)
{
   /// Check if the reward is valid
    if (string.IsNullOrEmpty(newPiece.description))
    {
        // If the reward is not valid, do not add it
        Debug.LogWarning("Trying to add an invalid reward to the roulette wheel.");
        return;
    }

    // Check if the reward is already on the roulette wheel
    if (roulettePieceData.Any(piece => piece.description == newPiece.description))
    {
        Debug.LogWarning($"The reward {newPiece.description} is already on the roulette wheel.");
        return;
    }

    // Add the new piece to the roulette piece data list
    roulettePieceData.Add(newPiece);

    // Add the new piece to the roulette wheel
    Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
    piece.GetComponent<RoulettePiece>().Setup(newPiece);
    piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * (roulettePieceData.Count - 1)));

    // Recalculate the weights and indices with the CalculateWeightsAndIndices method
    CalculateWeightsAndIndices();
}

private void ResetRouletteWheel()
{
    // Clear the roulette piece data list
    Debug.Log("청소");
    roulettePieceData.Clear();

    // Destroy all the roulette pieces and lines
    foreach (Transform child in pieceParent)
    {
        Destroy(child.gameObject);
    }

    foreach (Transform child in lineParent)
    {
        Destroy(child.gameObject);
    }

    roulettePieceData = new List<RoulettePieceData>
	{
    new RoulettePieceData { icon = gold, description = "Gold 300", rewardType = "Gold", rewardAmount = 300, chance = 25 },
    new RoulettePieceData { icon = gold, description = "Gold 500", rewardType = "Gold", rewardAmount = 500, chance = 35 },
    new RoulettePieceData { icon = gold, description = "Gold 1000", rewardType = "Gold", rewardAmount = 1000, chance = 40 }
	};
}
	private static Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}

    public void RewardAnim()
    {   if (!isAnimating) // 애니메이션이 실행 중이지 않을 때
    {
        rewardButton.interactable = false;
        spriteAnimation.Func_PlayUIAnim();
        audioSource.PlayOneShot(reward);
        StartCoroutine(DisablePanelAfterSeconds(3f));
        isAnimating = true;
    }
    else // 애니메이션이 실행 중일 때
    {
        // 마지막 프레임으로 이동하는 코드 (이 부분은 애니메이션 시스템에 따라 다르다)
        spriteAnimation.SkipToEnd(); // 이 함수는 실제로는 애니메이션 시스템에 따라 다를 수 있습니다.
        isAnimating = false; // 애니메이션 상태를 다시 false로 설정
    }

   
    }

     IEnumerator DisablePanelAfterSeconds(float seconds)
    {   
        yield return new WaitForSeconds(seconds);
    spriteAnimation.Func_ResetUIAnim(); // 애니메이션을 초기 상태로 되돌린다.
    rewardButton.interactable = true;
    rewardPanel.SetActive(false);
    isAnimating = false; // 애니메이션 상태를 다시 false로 설정
    if (GameController.Instance.currentjellyCount < 10)
     {
    StartCoroutine(WaitAndLoadMainScene(2f));
     }
    }

    public void UpdateRouletteWheel()
{
    // 기존 룰렛 섹션과 선을 제거
    foreach (Transform child in pieceParent)
    {
        Destroy(child.gameObject);
    }

    foreach (Transform child in lineParent)
    {
        Destroy(child.gameObject);
    }

    // 룰렛 데이터를 업데이트 (이 부분은 여러분의 로직에 따라 다를 수 있습니다)
    // 예: roulettePieceData = FetchNewRouletteData();

    // 새로운 룰렛 섹션과 선을 생성
    SpawnPiecesAndLines();

    // 새로운 룰렛 데이터로 가중치와 인덱스를 다시 계산
    CalculateWeightsAndIndices();
}
}

