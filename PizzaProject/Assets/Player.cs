using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Movement _movement;

    public Jumper Jumper => _jumper;
    public Movement Movement => _movement;

}
