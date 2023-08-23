using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoulettePiece : MonoBehaviour
{
	[SerializeField]
	private	Image			imageIcon;
	[SerializeField]
	private	TextMeshProUGUI	textDescription;
	[SerializeField]
    private Image background; // 이것은 피스의 배경색을 변경하기 위해 사용되는 Image 컴포넌트 참조입니다.

	public void Setup(RoulettePieceData pieceData)
	{
		imageIcon.sprite		= pieceData.icon;
		textDescription.text	= pieceData.description;
	}

	  public void SetColor(Color color)
    {
        if (background != null)
        {
            background.color = color;
        }
    }
}

