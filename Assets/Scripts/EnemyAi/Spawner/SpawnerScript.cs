using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // element 0 as fixer
	// element 1 as cop

	public string fixerSpawnPoint = "sometag";
	public string copSpawnPoint = "sometag";
	public bool alwaysSpawn = true;
	
	public List<GameObject> prefabsToSpawn;
	
    // Start is called before the first frame update
    void Start()
    {
		FixerSpawn();
    }

	private void Update() 
	{
		// current condition to sapwn cops
		if (Input.GetKeyDown(KeyCode.K)){CopSpawn();}
		 
	}

	private void FixerSpawn()
	{
		// Spawning Fixers

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(fixerSpawnPoint);
		foreach(GameObject spawnPoint in spawnPoints)
        {
			// int randomPrefab = Random.Range(0, prefabsToSpawn.Count);
            
			if(alwaysSpawn)
            {
				GameObject pts = Instantiate(prefabsToSpawn[0]);
				pts.transform.position = spawnPoint.transform.position;
			}
            else
            {
				int spawnOrNot = Random.Range(0, 2);
				if(spawnOrNot == 0)
                {
					GameObject pts = Instantiate(prefabsToSpawn[0]);
					pts.transform.position = spawnPoint.transform.position;
				}
			}
		}
	}

	private void CopSpawn()
	{
		// Spawning cop

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(copSpawnPoint);
		foreach(GameObject spawnPoint in spawnPoints)
        {
			// int randomPrefab = Random.Range(0, prefabsToSpawn.Count);
            
			if(alwaysSpawn)
            {
				GameObject pts = Instantiate(prefabsToSpawn[1]);
				pts.transform.position = spawnPoint.transform.position;
			}
            else
            {
				int spawnOrNot = Random.Range(0, 2);
				if(spawnOrNot == 0)
                {
					GameObject pts = Instantiate(prefabsToSpawn[1]);
					pts.transform.position = spawnPoint.transform.position;
				}
			}
		}
	}

}
