using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] objects;
    public Blue blue;
    public Gold gold;
    public Purple purple;
    public Vector2 center; // координаты центра
    public Vector2 size; // координаты в которых будут появляться объекты
    public GameObject zone;
    private int lenght = 0;
  
    private int random;
    void Start()
    {
        objects = new GameObject[8];
        objects[0] = new GameObject();
        objects[1] = new GameObject();
        objects[2] = new GameObject();
        objects[0] = blue.GameObject();
        objects[1] = gold.GameObject();
        objects[2] = purple.GameObject();
        lenght = 3;
    }

    public void Spawn()
    {
        Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        zone = objects[Random.Range(0, lenght)];
        Instantiate(zone, pos, Quaternion.identity); // осуществляем появление объекта в заданных случайных позициях в диапазоне заданных координат
        Vector2 pos2 = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2)); 
        Instantiate(gold, pos2, Quaternion.identity);
        Vector2 pos3 = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        Instantiate(purple, pos3, Quaternion.identity);
    }
}
