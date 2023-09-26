using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] objects;
    public Slowdown slowdaun;
    public BonusZone bonusZone;
    public InverceZone inverceZone;
    public Vector2 center; // координаты центра
    public Vector2 size = new Vector2(6.45f, 5.875f); // координаты в которых будут появляться объекты
    public GameObject zone;
    private GameObject obj;
    private int lenght = 0;
  
    private int random;
    void Start()
    {
        objects = new GameObject[8];
        objects[0] = new GameObject();
        objects[1] = new GameObject();
        objects[2] = new GameObject();
        objects[0] = slowdaun.GameObject();
        objects[1] = inverceZone.GameObject();
        objects[2] = bonusZone.GameObject();
        lenght = 3;
    }

    public void Spawn()
    {
        if(obj)
            Destroy(obj);
        Vector2 pos = center + new Vector2(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y));
        int number = Random.Range(0, lenght);
        zone = objects[number];
        obj = Instantiate(zone, pos, Quaternion.identity); // осуществляем появление объекта в заданных случайных позициях в диапазоне заданных координат
    }
}
