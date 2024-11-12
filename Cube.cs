using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnClicked;

    public int ChanceSplit { get; private set; } = 100;
    public Rigidbody Rigidbody { get; private set; }

    public void Initialize(int chance, Vector3 scale)
    {
        ChanceSplit = chance;
        transform.localScale = scale;

        GetComponent<Renderer>().material.color = Random.ColorHSV();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (TrySplit())
            OnClicked?.Invoke(this);
        else
            Destroy(gameObject);
    }

    private bool TrySplit()
    {
        return GetChance() <= ChanceSplit ? true : false;
    }

    private int GetChance()
    {
        int multiplier = 100;
        return (int)(Random.value * multiplier);
    }
}