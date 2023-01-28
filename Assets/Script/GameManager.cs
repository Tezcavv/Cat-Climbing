using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;



public class GameManager : MonoBehaviour
{
    
    private enum Directions {
        Left, Right
    }

    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject delCazzo;
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
        if (Input.GetKeyDown(KeyCode.D)) {
            RotateExagon(Directions.Right);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            RotateExagon(Directions.Left);
        }
        //if (Input.GetKey(KeyCode.W))
        //{
        //    terreni.ForEach(t => t.GetComponent<Movement>().Speed += 1 );
        //}else if (Input.GetKey(KeyCode.S))
        //{
        //    terreni.ForEach(t => t.GetComponent<Movement>().Speed -= 0.2f );
        //}

    }

    void RotateExagon(Directions direction) {

        Vector3 res= Vector3.zero;
        if (direction == Directions.Left) {
            res = Vector3.forward;
        }else if(direction == Directions.Right) {
            res = Vector3.back;
        }
        //uccidetemi
        foreach (GameObject terreno in terreni) {
            terreno.transform.RotateAround(delCazzo.transform.position, res, 60f);
        }



    }

    private IEnumerator ResetMovingBoolean() {

        yield return new WaitForSeconds(2);
        
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
        List<GameObject> listPlanes = new List<GameObject>();
        TerrainManager currentScript;
        foreach(Transform child in first.transform)
        {
            currentScript = child.GetComponent<TerrainManager>();
            currentScript.DeactivateAllObstacles();
            currentScript.SetObstacles(currentScript.currentObstacles);  
        }

        GameObject temp = first;
        terreni.Remove(first);
        terreni.Add(temp);

    }


}
