using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDefinition : MonoBehaviour
{
   [Header("UI Components")]
    public Image iconDisplay;
    public TextMeshProUGUI achievementNameText;
    public TextMeshProUGUI descriptionText;
    //public TextMeshProUGUI progressText;
    public Image[] stars; // 별 이미지 배열 (3개)
    public Slider progressBar; // 프로그레스 바

    [Header("Star Sprites")]
    public Sprite filledStar;  // 채워진 별 스프라이트
    public Sprite emptyStar;   // 빈 별 스프라이트

    private Achievement achievementInstance;

    public void SetAchievement(Achievement achievement)
    {
        this.achievementInstance = achievement;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (achievementInstance == null) return;

        //iconDisplay.sprite = achievementInstance.icon.sprite;
        achievementNameText.text = achievementInstance.achievementName;
        descriptionText.text = achievementInstance.description;

        int level = GetAchievementLevel();
        //progressText.text = "Level: " + level + " (" + achievementInstance.currentAmount + "/" + achievementInstance.requiredAmount + ")";
        UpdateStarDisplay(level);

        UpdateProgressBar();
    }

    private int GetAchievementLevel()
    {
        if (achievementInstance.currentAmount >= 100)
            return 3;
        else if (achievementInstance.currentAmount >= 10)
            return 2;
        else if (achievementInstance.currentAmount >= 1)
            return 1;
        else
            return 0;
    }

    private void UpdateStarDisplay(int level)
    {
        for (int i = 0; i < stars.Length; i++)
        {
        if (i < level) 
        {
            stars[i].sprite = filledStar;  // achievemnt fulfilled fill the star 
        }
        else
        {
            stars[i].sprite = emptyStar;   // achievemnt fulfilled fill the star 
        }
        }
    }

    private void UpdateProgressBar()
    {
        if(progressBar != null)
        {
            progressBar.maxValue = achievementInstance.requiredAmount;
            progressBar.value = achievementInstance.currentAmount;
        }
    }
    
}