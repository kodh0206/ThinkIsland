using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VegetableCollection", menuName = "Vegetable Collection")]
public class CropsCollection : ScriptableObject
{
    [SerializeField]
    public List<CropData> objects;

}