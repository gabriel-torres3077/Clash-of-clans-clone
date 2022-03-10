using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GerenciadorSalve : MonoBehaviour {

	public static GerenciadorSalve inst;
	public List<ClasseSalve> salveObjs;

	void Awake () {		
		

		if(inst == null)
		{
			inst = this;
		}
		else if(inst != this)
		{
			Destroy(gameObject);
		}

		salveObjs = new List<ClasseSalve>();
	}

	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Salvar()
	{
		
		
		PlayerPrefs.SetInt("ObjetosCena",salveObjs.Count);
		
		for(int i = 0; i < salveObjs.Count; i++)
		{
			salveObjs[i].Salvar(i);
		}
		print("salvei");
	}

	public void Carrega()
	{

		foreach(ClasseSalve obj in salveObjs)
		{
			if(obj != null)
			{
				Destroy(obj.gameObject);
			}			
		}

		salveObjs.Clear();

		int objetosQuant = PlayerPrefs.GetInt("ObjetosCena");		

		for(int x = 0; x < objetosQuant; x++)
		{
			
			string[] valor = PlayerPrefs.GetString(x.ToString()).Split('_');			
			GameObject temp = null;

			switch(valor[0])
			{
				case "CasaNormal":
					 temp = Instantiate(Resources.Load("ObjPos3") as GameObject);
				break;
				case "CasaMoeda":
					 temp = Instantiate(Resources.Load("ObjPosMoedas") as GameObject);
				break;
			}

			if(temp != null)
			{
				temp.GetComponent<ClasseSalve>().Carrega(valor);
			}

			print(valor);
			//GameObject prefab = Instantiate(Resources.Load("ObjPos3") as GameObject);
			temp.GetComponent<ClasseSalve>().Carrega(valor);
			temp.GetComponent<Grid>().construcaoFinalizada = true;
		}
	}

	public Vector3 StringParaVector(string s)
	{
		s = s.Trim(new char[] {'(',')'});
		s = s.Replace(" ","");
		string[] pos = s.Split(',');

		return new Vector3(float.Parse(pos[0]),float.Parse(pos[1]),float.Parse(pos[2]));
	}

	/*public Quaternion StringParaQuaternion(string s)
	{
		return Quaternion.identity;
	}*/
}
