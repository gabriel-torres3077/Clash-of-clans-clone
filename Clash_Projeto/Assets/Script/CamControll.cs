using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamControll : MonoBehaviour {


	
	void Update () {

		if(Input.GetMouseButton(0) && !Gamemanager.inst.itemSelecionado)
		{
			transform.Translate(-Input.GetAxis("Mouse X") * Time.deltaTime * 10,0,-Input.GetAxis("Mouse Y") * Time.deltaTime * 10);	
		}	


		LimitaPosicao();

	}

	void LimitaPosicao()
	{
		Vector3 pos = transform.position;
    	pos.x =  Mathf.Clamp(transform.position.x, -52f,-43f);
		pos.z =  Mathf.Clamp(transform.position.z, -52f,-49f);
		pos.y = 74.1f;
    	transform.position = pos;
	}
}



       