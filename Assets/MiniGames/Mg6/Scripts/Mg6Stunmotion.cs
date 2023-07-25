using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Stunmotion : MonoBehaviour
{
    Vector2 Target;
    // Start is called before the first frame update
    void Start()
    {
        Mg6Player Player= GetComponent<Mg6Player>();
        Target= Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Mg6Player Player = GetComponent<Mg6Player>();
        Target = Player.transform.position;
        transform.position = new Vector2(Target.x, Target.y + 0.7f);
    }
}
