using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using System;

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
	private	RoulettePieceData[]	roulettePieceData;			// �귿�� ǥ�õǴ� ���� �迭

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

	private void Awake()
	{
	roulettePieceData = new RoulettePieceData[]
    	{
        new RoulettePieceData { icon = null, description = "Gold 300", rewardType = "Gold", rewardAmount = 300, chance = 25 },
        new RoulettePieceData { icon = null, description = "Gold 500", rewardType = "Gold", rewardAmount = 500, chance = 35 },
        new RoulettePieceData { icon = null, description = "Gold 1000", rewardType = "Gold", rewardAmount = 1000, chance = 40 }
    	};

    	pieceAngle = 360 / roulettePieceData.Length;
    	halfPieceAngle = pieceAngle * 0.5f;
    	halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);
    	SpawnPiecesAndLines();
    	CalculateWeightsAndIndices();
	}

	private void Update()
	{
	if (RewardManager.Instance.HasNewRewards())
        {
            // RewardManager에서 새로운 보상을 가져와서 룰렛에 추가합니다.
            List<LevelRewardData> newRewards = RewardManager.Instance.GetNewRewards();
            foreach (LevelRewardData reward in newRewards)
            {
                AddPiece(reward);
            }

            // 보상이 추가되었으므로 pieceAngle과 halfPieceAngle을 다시 계산합니다.
            pieceAngle = 360 / roulettePieceData.Length;
            halfPieceAngle = pieceAngle * 0.5f;
            halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

            // 보상이 추가되었으므로 룰렛 조각과 선을 다시 생성합니다.
            SpawnPiecesAndLines();
        }
    }
	
	private void SpawnPiecesAndLines()
	{
		for ( int i = 0; i < roulettePieceData.Length; ++ i )
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
		for ( int i = 0; i < roulettePieceData.Length; ++ i )
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

		for ( int i = 0; i < roulettePieceData.Length; ++ i )
		{
			if ( roulettePieceData[i].weight > weight )
			{
				return i;
			}
		}

		return 0;
	}
 public void Spin(UnityAction<RoulettePieceData> endOfSpinCallback)
    {
        if (isSpinning) return;  // 이미 회전중이라면 회전을 시작하지 않는다.

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
    switch (selectedReward.rewardType)
    {
        case "Gold":
            // 골드 보상 처리
            // 예를 들어 플레이어의 골드를 증가시키는 코드 등
            break;
        case "Crop":
			 CropData newCrop = GameController.Instance.CropList.Find(c => c.plantName == selectedReward.description);
            if (newCrop != null)
            {
                GameController.Instance.currentUnlockedCrops.Add(newCrop);
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

void AddPiece(LevelRewardData reward)
{
   RoulettePieceData newPiece = new RoulettePieceData();

        // LevelRewardData에서 RoulettePieceData로 정보를 복사합니다.
        newPiece.description = reward.unlockedCrop != null ? reward.unlockedCrop :
                                reward.unlockedMiniGame;
        newPiece.icon = reward.unlockedCrop != null ? reward.CropIcon :
                        reward.MiniGameIcon;
        newPiece.rewardType = reward.unlockedCrop != null ? "Crop" :
                              "MiniGame";
        newPiece.chance = 1;
    // Add the new piece to the roulette piece data array
    Array.Resize(ref roulettePieceData, roulettePieceData.Length + 1);
    roulettePieceData[roulettePieceData.Length - 1] = newPiece;

    // Add the new piece to the roulette wheel
    // This could be done similarly to the SpawnPiecesAndLines method
    Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
    piece.GetComponent<RoulettePiece>().Setup(newPiece);
    piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * (roulettePieceData.Length - 1)));

    // Recalculate the weights and indices with the CalculateWeightsAndIndices method
    CalculateWeightsAndIndices();
	}
}

