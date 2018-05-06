using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//Make this into a butterfly demo
public class PopulationManager : MonoBehaviour {

    public GameObject personPrefab;
    //Size to tell how many object you want spawned in
    public int populationSize = 10;
    //List to store objects
    List<GameObject> population = new List<GameObject>();
    //Keep track of how much time has passed
    public static float elapsed;
    //How long we want each game to last
    public int trialTime = 10;
    //Keep track of what generation we are on
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 60, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
        GUI.Label(new Rect(10, 550, 100, 20), "Select objects with genes to pass down",guiStyle);
    }


    // Use this for initialization
    void Start () {
        //Creates 10 objects, each with random rgb values and adds them to our population  list
        for(int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-7, 7), Random.Range(-2.5f, 3.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().size = Random.Range(0.1f, 0.4f);
            population.Add(go);
        }

		
	}

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-7, 7), Random.Range(-3.5f, 3.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        //swap parent dna
        //Mutation
        //Genetic Algorithm
        //To increase mututations, decrease the size of the range and increase the target.
        if(Random.Range(0,1000) > 5)
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA>().size = Random.Range(0, 10) < 5 ? dna1.size : dna2.size;
        }
        //Mutation
        else
        {
            offspring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().size = Random.Range(0.1f, 0.4f);

        }


        return offspring;
    }

    void BreedNewPopulation()
    {
        //List hold new population
        List<GameObject> newPopulation = new List<GameObject>();
        //get rid of unfit individuals
        //Trains objects to breed based on last clicked rather than first
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        //Trains objects to breed based on first clicked rather than last
        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();
        //breed upper half of sorted list
        for(int i = (int) (sortedList.Count / 2.0f) -1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        //destory all parents and previous population
        for(int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
	
	// Update is called once per frame
	void Update () {
        //Adds time between frames
        elapsed += Time.deltaTime;
        //If our game has been going longer than our alloted trial time, generate a new population and set elapsed back to 0
        if(elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
	}
}
