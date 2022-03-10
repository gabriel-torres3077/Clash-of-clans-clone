using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Destino : MonoBehaviour {

	public NavMeshAgent agent;
	public Transform alvo;

	void Start () {
		
		

	}
	
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			agent.SetDestination(alvo.position);
		}

	}
}
