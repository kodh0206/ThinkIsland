using UnityEngine;

public class FieldManager : MonoBehaviour {
    public Field[,] farmTiles;
    public int money;
    public GameObject tilePrefab;
    public int width;
    public int height;

    void Start() {
        farmTiles = new Field[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                GameObject tileObj = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                farmTiles[x, y] = tileObj.GetComponent<Field>();
            }
        }
        money = 0;
    }
/*
    // method to plant a crop on a tile
    public void PlantCrop(int x, int y, Crop crop) {
        farmTiles[x, y].PlantCrop(crop);
    }

    // method to harvest a crop from a tile
    public void HarvestCrop(int x, int y) {
        if (farmTiles[x, y].IsReadyToHarvest()) {
            Crop harvestedCrop = farmTiles[x, y].HarvestCrop();
            money += harvestedCrop.price;
        }
    }
    ]
    */
}