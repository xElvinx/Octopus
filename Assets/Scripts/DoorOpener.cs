using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public List<Character> enemies;

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        foreach (var enemy in enemies)
        {
            if (!enemy.isActiveAndEnabled)
            {
                counter++;
            }
        }

        if (counter >= enemies.Count)
        {
            gameObject.SetActive(false);
        }
    }
}
