using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    [Range(1, 40)]
    private int numEsagoni;
    List<GameObject> terreni;

    GameObject first => terreni[0];
    GameObject last => terreni.LastOrDefault();


    private void Start() {
       SpawnTerrain();
    }

    private void Update() {
        if (first.transform.position.z <=-100) {
            ResetTerrain();
        }
    }


    public void SpawnTerrain() {
        terreni = new List<GameObject>();

        GameObject temp;
        for (int i = 0; i < numEsagoni; i++) {
            temp = Instantiate(prefab, new Vector3(0, 0, i * 100), Quaternion.identity);
            terreni.Add(temp);
        }
    }

    public void ResetTerrain() {
        first.transform.position = last.transform.position + new Vector3(0,0,100);

        GameObject temp = first;
        terreni.Remove(first);
        terreni.Add(temp);

    }
}
