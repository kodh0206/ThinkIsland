using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "AchievementList", menuName = "Achievements/New Achievement List")]
public class AchievementList : ScriptableObject
{
    public List<Achievement> achievements;
}