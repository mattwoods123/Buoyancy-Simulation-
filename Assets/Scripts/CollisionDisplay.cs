using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionDisplay : MonoBehaviour
{
    public TextMeshProUGUI dis;
    int score = 0;

    public void addScore()
    {
        score++;

        dis.text = "Collisions: " + score;
    }
}
