using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;       
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor; //mouse, karenin üzerine gelince renk değişsin
    }

    private void OnMouseExit()
    {
        sr.color = startColor; //mouse, karenin üzerinden çıkınca renk eski haline dönsün
    }

    //kule spawnlama kodlarımız burda
    private void OnMouseDown()
    {
        Debug.Log("build tower here." + name);
        //hadi kuleyi yerleştirelim
        if (tower != null) return; //kule varsa boş döndür
        GameObject towerToBuild = BuildingManager.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }


}
