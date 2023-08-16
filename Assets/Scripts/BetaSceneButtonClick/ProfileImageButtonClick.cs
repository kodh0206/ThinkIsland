using UnityEngine;
using UnityEngine.UI;

// BetaScene -> Profile Canvas (off) -> Profile Panel -> Photo Select Panel -> Scroll View -> Viewport -> Content에 Button 5개 추가 (미니게임 20개)


// BetaScene -> Profile Canvas (off) -> Profile Panel -> Photo Select Panel -> Scroll View -> Viewport -> Content에 스크립트 할당
// BetaScene -> Profile Canvas (off) -> Profile Panel -> Photo Select Panel -> Scroll View -> Viewport -> Content -> Button ~ Button (19) SetActive (false) 로 해두기
public class ProfileImageButtonClick : MonoBehaviour
{
    // Profile Canvas (off) -> Profile photo -> Button(1) 할당
    public Button profileImage;

    // BetaScene -> Profile Canvas (off) -> Profile Panel -> Photo Select Panel -> Scroll View -> Viewport -> Content -> Button ~ Button (19)
    public Button[] changeImageButtons;

    private void Start()
    {

        // 프로필사진 변경
        foreach (Button button in changeImageButtons)
        {
            button.onClick.AddListener(() => CopyImage(button));
        }

        for (int i = 0; i < changeImageButtons.Length; i++)
        {
            // GameController 리스트 안에 해금된 MiniGames 확인
            string characterName = "Mg" + (i + 1);
            if (GameController.Instance.unlockedMiniGames.Contains(characterName))
            {
                changeImageButtons[i].gameObject.SetActive(true);
            }
        }
    }

    private void CopyImage(Button clickedButton)
    {
        Image image1 = profileImage.GetComponent<Image>();
        Image clickedImage = clickedButton.GetComponent<Image>();

        image1.sprite = clickedImage.sprite;
    }
}
