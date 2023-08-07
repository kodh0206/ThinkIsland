using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/size:up/트와이스!, /size:init/my name is Teemo.", "Teemo"));

        dialogTexts.Add(new DialogData("Yeah Yeah Yeah Yeah /Come and be my love /Come and be my love baby", "Teemo"));
        
        dialogTexts.Add(new DialogData("멍하니 서서 막 고민고민 해~ 나 이거 진짜 잘하는 짓인지", "Teemo"));

        dialogTexts.Add(new DialogData("You can easily change text /color:red/color, /color:white/and /size:up//size:up/size/size:init/ like this.", "Teemo"));

        dialogTexts.Add(new DialogData("Just put the command in the string!", "Teemo"));

        dialogTexts.Add(new DialogData("You can also change the character's sprite /emote:Sad/like this, /click//emote:Happy/Smile.", "Teemo",  () => Show_Example(2)));

        dialogTexts.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Teemo"));

        dialogTexts.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Teemo"));

        dialogTexts.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Teemo" ));

        dialogTexts.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Teemo"));

        dialogTexts.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha.", "Teemo" ));

        dialogTexts.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Teemo"));

        DialogManager.Show(dialogTexts);
    }

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
