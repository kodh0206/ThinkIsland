using UnityEngine;

public class RandomObject : MonoBehaviour
{
    private int[] numbers = { 1, 2, 3 };
    private float[] ratios = { 1f, 2.2f, 2.2f };

    public int GetRandomNumber()
    {
        int randomIndex = GetRandomIndexByRatios();
        int randomNumber = numbers[randomIndex];
        return randomNumber;
    }

    int GetRandomIndexByRatios()
    {
        float totalRatio = 0f;

        foreach (float ratio in ratios)
        {
            totalRatio += ratio;
        }

        float randomValue = Random.Range(0f, totalRatio);

        for (int i = 0; i < ratios.Length; i++)
        {
            if (randomValue < ratios[i])
            {
                return i;
            }

            randomValue -= ratios[i];
        }

        return ratios.Length - 1;
    }
}
