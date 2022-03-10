using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class CodigoAtaque : MonoBehaviour {

	public NavMeshAgent agent;
	public float distanciaMinima;

	public List<float> dists;
	public GameObject mataObj;
	private float distMin;
	public Grid_Inimigo gi;
	

	void Start()
	{
		
		Calculo();
		
	}
	
	void Update () 
	{	
		if(!mataObj)
		{
			dists.Clear();
			Calculo();
		}
	}

void Calculo()
{
	print("Me chamaram");
	for(int x = 0; x < Gamemanager.inst.constInimiga.Count; x++)
	{
		distanciaMinima = Vector3.Distance(transform.position,Gamemanager.inst.constInimiga[x].transform.position);

		dists.Add(distanciaMinima);

		distMin = dists.Min();
		
		if(Vector3.Distance(transform.position,Gamemanager.inst.constInimiga[x].transform.position) == distMin)
		{
						
			agent.SetDestination(Gamemanager.inst.constInimiga[x].transform.position);
			mataObj = Gamemanager.inst.constInimiga[x];
			transform.LookAt(Gamemanager.inst.constInimiga[x].transform);
		}

			
	}
}

void OnTriggerEnter(Collider col)
{
	if(col.gameObject.CompareTag("inimigobase"))
	{		
		gi = col.GetComponent<Grid_Inimigo>();
		gi.InvokeRepeating("RecebeDanos",0.1f,0.5f);			
	}

}

}
