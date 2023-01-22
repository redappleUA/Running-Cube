using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform Begin; //Start of chunk (set on each chunk)
    public Transform End; //End of chunk (set on each chunk)
    public AnimationCurve ChanceFromDistance;

    [SerializeField] Transform _wallSpawn;
    [SerializeField] GameObject[] _walls; //Different options of walls
    [SerializeField] Transform _cubesSpawn;
    [SerializeField] GameObject[] _cubes; //Different options of cubes

    private static int lastValue;

    private void Start()
    {
        Instantiate(_walls[UniqueRandom(0, _walls.Length)], _wallSpawn.position, Quaternion.identity);
        Instantiate(_cubes[UniqueRandom(0, _cubes.Length)], _cubesSpawn.position, Quaternion.identity);
    }

    /// <summary>
    /// Random.Range without duplicate
    /// </summary>
    /// <param name="min">min range</param>
    /// <param name="max">max range</param>
    /// <returns></returns>
    int UniqueRandom(int min, int max)
    {
        if(min == max)
        {
            Debug.LogError("Unique Random: min == max");
            return 0;
        }
        else
        {
            int val = Random.Range(min, max);
            while (lastValue == val)
            {
                val = Random.Range(min, max);
            }
            lastValue = val;
            return val;
        }
    }
}