using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeTrap : MonoBehaviour
{
    public List<GameObject> spikeObjects; // Các object chông (Spike1, Spike2)
    private bool activated = false;

    void Start()
    {
        foreach (var spike in spikeObjects)
        {
            // Ẩn sprite + collider
            spike.GetComponent<SpriteRenderer>().enabled = false;

            Collider2D col = spike.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            foreach (var spike in spikeObjects)
            {
                spike.GetComponent<SpriteRenderer>().enabled = true;

                Collider2D col = spike.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = true;
            }
        }
    }
}
