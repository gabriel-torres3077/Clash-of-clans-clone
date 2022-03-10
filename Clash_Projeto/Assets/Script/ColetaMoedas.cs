using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColetaMoedas : MonoBehaviour {



    [SerializeField]
    private float tempoCont,tempoAtualCont;

    [SerializeField]
    private bool pegueMoeda;
    [SerializeField]
    private GameObject moedaImg;
	
	[SerializeField]
	private Grid construcaoMoeda;
	[SerializeField]
	bool inicioTempo;

	public Nivel[] nivel;
	public int nivelInt;

	

	void Start () {
		
		if(PlayerPrefs.HasKey("nivel"))
		{
			nivelInt = PlayerPrefs.GetInt("nivel");			
		}
		else
		{
			PlayerPrefs.SetInt("nivel",0);
			nivelInt = PlayerPrefs.GetInt("nivel");	
		}

		tempoAtualCont = nivel[nivelInt].meuTempo;
		

        pegueMoeda = false;
		tempoCont = tempoAtualCont;
		moedaImg.SetActive(false);
		construcaoMoeda = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {

		TempoMoedas();

	}

	   //Calculo de moedas

    void TempoMoedas()
    {
		if(construcaoMoeda.construcaoFinalizada)
		{	
			inicioTempo= true;
		}

		if(!pegueMoeda)
		{
			moedaImg.SetActive(false);

			if(inicioTempo)
			{
				tempoCont -= Time.deltaTime;	
				if(tempoCont <= 0)
				{
					pegueMoeda = true;                
					tempoCont = 0;
					inicioTempo = false;
				}
			}

		}
		else
		{
			moedaImg.SetActive(true);
		}



    }

    public void ColetaMoeda(int quant)
    {
        pegueMoeda = false;        
        Gamemanager.inst.moedas += quant;
		tempoCont = tempoAtualCont;
		PlayerPrefs.SetInt("moedas",Gamemanager.inst.moedas);
		Gamemanager.inst.moedasUI.text = PlayerPrefs.GetInt("moedas").ToString();
		moedaImg.SetActive(false);
    }

	public void ControlaNivel()
	{
		nivelInt++;
		nivelInt = Mathf.Clamp(nivelInt,0,2);
		PlayerPrefs.SetInt("nivel",nivelInt);
		nivelInt = PlayerPrefs.GetInt("nivel");	
		tempoAtualCont = nivel[nivelInt].meuTempo;
		construcaoMoeda.canvasEvolucao.SetActive(false);
	}
}
