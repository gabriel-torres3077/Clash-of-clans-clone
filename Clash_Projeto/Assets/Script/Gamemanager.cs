using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour {

	public static Gamemanager inst;	

	public GameObject painelCompra;

	public bool nascimento;

	

	public Transform objInst;

	public bool itemSelecionado;

	public Text moedasUI;	
    
    public int moedas;

	public List<GameObject> constInimiga;

	public int guerreirosNum;
	

	void Awake()
	{
		

		if(inst == null)
		{
			inst = this;
		}
		else if(inst != this)
		{
			Destroy(gameObject);
		}

		constInimiga.AddRange(GameObject.FindGameObjectsWithTag("inimigobase"));
		guerreirosNum = 10;

		
		
	}

	void Start () {

	#region ..
	if(SceneManager.GetActiveScene().buildIndex == 0)
	{
		GerenciadorSalve.inst.Carrega();
	}
		
	#endregion
		
		moedas = PlayerPrefs.GetInt("moedas");

		if(PlayerPrefs.HasKey("moedas"))
		{
			moedasUI.text = PlayerPrefs.GetInt("moedas").ToString();
		}
		else
		{
			moedasUI.text = "0";
		}

		painelCompra = GameObject.FindGameObjectWithTag("PainelCompra");

		if(painelCompra != null)
		{
			painelCompra.SetActive(false);
		}	
		
		
		
	}	


	void Update () {
		

	}

	public void CriaObjetoEmCena(Transform obj)
	{		
		CriaObjCena(obj);
	}


	void CriaObjCena(Transform obj)
	{
		objInst =(Transform) Instantiate(obj,new Vector3(Camera.main.transform.localPosition.x/* + 60,0.02f*/,Camera.main.transform.localPosition.z/* + 60*/),transform.rotation);	
		nascimento = true;
		painelCompra.SetActive(false);
	}

	public void MostraLoja()
	{
		painelCompra.SetActive(true);
	}




}
