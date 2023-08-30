using UnityEngine;
using UnityEngine.UI;
// Building Class
public class Building : MonoBehaviour
{

    public int currentLevel = 0;
    public int maxLevel = 3;
    public int currentUnlockedGames = 0;
    public Sprite[] buildingSprites;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public GameObject upgradeButton;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateBuildingSprite();

        // 버튼 초기 상태 설정
        upgradeButton.SetActive(false);
        Button upgradeBtn = upgradeButton.GetComponent<Button>();
        upgradeBtn.onClick.AddListener(UpgradeBuilding);  // 버튼 클릭 이벤트에 UpgradeBuilding 함수 연결
    }
    private void Update()
    {
        // 모든 터치에 대해서 반복문 실행
       foreach (Touch touch in Input.touches)
    {
      
 
    if (touch.phase == TouchPhase.Began)
    {
        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero); // Change here

        if (hitInformation.collider == boxCollider)
        {
            ToggleUpgradeButton();
        }
    }
    }
    }

    private void OnMouseDown()
    {
        // 클릭 시 업그레이드 버튼 표시
       ToggleUpgradeButton();
    }

    public void UpgradeBuilding()
    {   Debug.Log("클릭됨");
        if (currentLevel < maxLevel)//여기에 업그레이드 조건도 추가 
        {
            currentLevel++;
            currentUnlockedGames++;
            UpdateBuildingSprite();
          
        }
        else
        {
            Debug.Log("건물이 이미 최대 레벨입니다.");
        }

        
    }

     void UpdateBuildingSprite()
    {
        if (buildingSprites != null && currentLevel < buildingSprites.Length)
        {
            spriteRenderer.sprite = buildingSprites[currentLevel];

            // 스프라이트의 크기에 맞게 콜라이더 크기 조정
            boxCollider.size = spriteRenderer.sprite.bounds.size;
        }
        else
        {
            Debug.LogError("스프라이트 설정에 문제가 있습니다.");
        }
    }

   private void DisplayUpgradeButton()
    {
       if (upgradeButton.activeInHierarchy)
    {
        upgradeButton.SetActive(false);  // 이미 활성화되어 있으면 비활성화
    }
    else
    {
        upgradeButton.SetActive(true);  // 비활성화되어 있으면 활성화
    }
    }

    public void OnUpgradeButtonClick()
{
    UpgradeBuilding();
    upgradeButton.SetActive(false); // 업그레이드 버튼을 클릭한 후 버튼을 다시 숨깁니다.
}
    void ToggleUpgradeButton()
{
    if (upgradeButton.activeSelf) // 이미 활성화되어 있다면
    {
        upgradeButton.SetActive(false); // 비활성화
    }
    else // 비활성화되어 있다면
    {
        upgradeButton.SetActive(true); // 활성화
    }
}
}
