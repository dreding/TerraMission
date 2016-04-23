using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Planet : MonoBehaviour {

    List<Sattelite> _lNatureSattelites = new List<Sattelite>(2);

    List<Sattelite> _lArtificialSattelites = new List<Sattelite>();

    public string name;
    public float radius; // в км
    public float dayLenght; // в минутах
    public float yearLenght; // в сутках
    public float angle;
    public float orbitalSpeed;// km/s

    protected float square; //общая площадь поверхности планеты в км кв

    void Update()
    {
        RotatePlanet();
    }

    protected void CalculateSquare()
    {
 
    }

    protected void RotatePlanet()
    {
 
    }
}
