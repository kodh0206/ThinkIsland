using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
	[SerializeField]
	private	Roulette	roulette;
	[SerializeField]
	private	Button		buttonSpin;
	[SerializeField]
    private UISpriteAnimation spriteAnimation;  // 애니메이션 컴포넌트 참조 추가

	private void Awake()
	{
		   buttonSpin.onClick.AddListener(() =>
    {
        buttonSpin.interactable = false;
		spriteAnimation.Func_PlayUIAnim();
        roulette.Spin(EndOfSpin);
    });
	}

	private void EndOfSpin(RoulettePieceData selectedData)
	{
		spriteAnimation.Func_StopUIAnim();
		if (GameController.Instance.currentjellyCount < 10)
     	{
			buttonSpin.interactable = false;
    		
     	}
	
		
    Debug.Log($"{selectedData.index}:{selectedData.description}");
	buttonSpin.interactable = true;
    
  
	}
}

