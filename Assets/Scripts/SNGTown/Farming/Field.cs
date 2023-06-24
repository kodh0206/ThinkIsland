using System;
using UnityEngine;
public enum FieldState
{
    Locked,  // 빈 상태 (작물이 심어지지 않음)
    Empty,  // 작물이 심어진 상태
    Planted  // 작물 수확 가능한 상태
}

public class Field:MonoBehaviour
{
    public int serialNo;
    public Crops crops;
    public FieldState fieldState;

    //새 밭
    public Field(int serialNo, Crops crops, int seedPrice, int harvestPrice )
    {
        this.serialNo = serialNo;
        this.crops = crops;
        this.fieldState =FieldState.Empty;
    }

    /*구현해야될 함수
    1.상태에 따라 다른 스프라이트 보여주기
        crops의 남은 시간에 따라 시간을 3/1씩 나눠서 




    */
}


