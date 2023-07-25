using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

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

	private	float				pieceAngle;					// ���� �ϳ��� ��ġ�Ǵ� ����
	private	float				halfPieceAngle;				// ���� �ϳ��� ��ġ�Ǵ� ������ ���� ũ��
	private	float				halfPieceAngleWithPaddings;	// ���� ���⸦ ������ Padding�� ���Ե� ���� ũ��
	
	private	int					accumulatedWeight;			// ����ġ ����� ���� ����
	private	bool				isSpinning = false;			// ���� ȸ��������
	private	int					selectedIndex = 0;			// �귿���� ���õ� ������

	public Sprite gold;
	private void Awake()
	{
	roulettePieceData = new List<RoulettePieceData>
	{
    new RoulettePieceData { icon = gold, description = "Gold 300", rewardType = "Gold", rewardAmount = 300, chance = 25 },
    new RoulettePieceData { icon = gold, description = "Gold 500", rewardType = "Gold", rewardAmount = 500, chance = 35 },
    new RoulettePieceData { icon = gold, description = "Gold 1000", rewardType = "Gold", rewardAmount = 1000, chance = 40 }
	};
    	pieceAngle = 360 / roulettePieceData.Count;
    	halfPieceAngle = pieceAngle * 0.5f;
    	halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);
    	SpawnPiecesAndLines();
    	CalculateWeightsAndIndices();
	}

	private void Update()
	{
		if (RewardManager.Instance.HasNewRewards())
	{
		// 룰렛 휠 초기화
		ResetRouletteWheel();
	
		// RewardManager에서 새로운 보상을 가져와서 룰렛에 추가합니다.
		List<LevelRewardData> newRewards = RewardManager.Instance.GetNewRewards();

        // LevelRewardData를 RoulettePieceData로 변환
        List<RoulettePieceData> convertedRewards = RewardManager.Instance.ConvertLevelRewardsToPieces(newRewards);

        // 변환된 보상들을 룰렛에 추가
        foreach (RoulettePieceData reward in convertedRewards)
		{
			AddPiece(reward);
		}

		// 보상이 추가되었으므로 pieceAngle과 halfPieceAngle을 다시 계산합니다.
		pieceAngle = 360 / roulettePieceData.Count;
		halfPieceAngle = pieceAngle * 0.5f;
		halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

		// 보상이 추가되었으므로 룰렛 조각과 선을 다시 생성합니다.
		SpawnPiecesAndLines();
	}
}
	
	private void SpawnPiecesAndLines()
	{
		for ( int i = 0; i < roulettePieceData.Count; ++ i )
		{
			Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
			// ������ �귿 ������ ���� ���� (������, ����)
			piece.GetComponent<RoulettePiece>().Setup(roulettePieceData[i]);
			// ������ �귿 ���� ȸ��
			piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));

			Transform line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);
			// ������ �� ȸ�� (�귿 ���� ���̸� �����ϴ� �뵵)
			line.RotateAround(lineParent.position, Vector3.back, (pieceAngle * i) + halfPieceAngle);
		}
	}

	private void CalculateWeightsAndIndices()
	{
		for ( int i = 0; i < roulettePieceData.Count; ++ i )
		{
			roulettePieceData[i].index = i;

			// ����ó��. Ȥ�ö� chance���� 0 �����̸� 1�� ����
			if ( roulettePieceData[i].chance <= 0 )
			{
				roulettePieceData[i].chance = 1;
			}

			accumulatedWeight += roulettePieceData[i].chance;
			roulettePieceData[i].weight = accumulatedWeight;

			Debug.Log($"({roulettePieceData[i].index}){roulettePieceData[i].description}:{roulettePieceData[i].weight}");
		}
	}

	private int GetRandomIndex()
	{
		int weight = UnityEngine.Random.Range(0, accumulatedWeight);

		for ( int i = 0; i < roulettePieceData.Count; ++ i )
		{
			if ( roulettePieceData[i].weight > weight )
			{
				return i;
			}
		}

		return 0;
	}
 public void Spin(UnityAction<RoulettePieceData> endOfSpinCallback)
    {  Debug.Log("Spin method called. Is spinning: " + isSpinning);
        if (isSpinning) return;

        isSpinning = true;
        selectedIndex = GetRandomIndex();  // 랜덤한 인덱스를 선택

        float spinAngle = 360 * (spinDuration - 1);  // 스핀할 각도
        spinAngle += pieceAngle * selectedIndex;  // 선택한 인덱스에 따라 스핀 각도 조절
        spinAngle -= spinningRoulette.rotation.eulerAngles.z;  // 현재 회전 각도를 고려

        StartCoroutine(OnSpin(spinAngle, endOfSpinCallback));
    }
private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
{
    float current = 0;
    float percent = 0;

    while ( percent < 1 )
    {
        current += Time.deltaTime;
        percent = current / spinDuration;

        float z = Mathf.Lerp(0, end, spinningCurve.Evaluate(percent));
        spinningRoulette.rotation = Quaternion.Euler(0, 0, z);

        yield return null;
    }

    isSpinning = false;

    if ( action != null ) action.Invoke(roulettePieceData[selectedIndex]);

    // 보상 처리 부분
   RoulettePieceData selectedReward = roulettePieceData[selectedIndex];
roulettePieceData.RemoveAt(selectedIndex);

// Find the reward in the newRewards list that matches the selected reward
LevelRewardData rewardToRemove = RewardManager.Instance.GetMatchedNewReward(selectedReward.description);

// If the reward is in the list, remove it
if (rewardToRemove != null)
{
    RewardManager.Instance.RemoveFromNewRewards(rewardToRemove);
}

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
        Debug.Log("Unlocked new minigame: " + selectedReward.description);
   	 	}
    break;
           
        default:
            break;
    }

    StartCoroutine(WaitAndLoadMainScene(2f));
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



}

