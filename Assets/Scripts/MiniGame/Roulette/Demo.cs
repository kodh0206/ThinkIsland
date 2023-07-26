using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
	[SerializeField]
	private	Roulette	roulette;
	[SerializeField]
	private	Button		buttonSpin;

	private void Awake()
	{
		   buttonSpin.onClick.AddListener(() =>
    {
        buttonSpin.interactable = false;
        roulette.Spin(EndOfSpin);
    });
	}

	private void EndOfSpin(RoulettePieceData selectedData)
	{
		  buttonSpin.interactable = true;

    Debug.Log($"{selectedData.index}:{selectedData.description}");

    // 보상 유형에 따라 다른 작업을 수행합니다.
    switch (selectedData.rewardType)
    {
        case "Crop":
            Debug.Log($"You have unlocked a new crop: {selectedData.description}");
            // 새로운 작물을 해금하는 로직을 추가합니다.
            break;
        case "MiniGame":
            Debug.Log($"You have unlocked a new mini-game: {selectedData.description}");
            // 새로운 미니게임을 해금하는 로직을 추가합니다.
            break;
        // 기타 보상 유형에 대한 처리를 추가합니다.
        default:
            Debug.Log($"Unknown reward type: {selectedData.rewardType}");
            break;
    }
	}
}

