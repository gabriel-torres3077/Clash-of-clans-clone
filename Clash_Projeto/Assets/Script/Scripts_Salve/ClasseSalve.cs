using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TipoObj
{
	CasaNormal,CasaMoeda
}

public abstract class ClasseSalve : MonoBehaviour {

	protected string salve;
	[SerializeField]
	private TipoObj objTipo;

	
	// Use this for initialization


	public virtual void Salvar(int id)
	{
		PlayerPrefs.SetString(id.ToString(),objTipo +"_"+ transform.position.ToString());
	}

	public virtual void Carrega(string[] valores)
	{
		transform.localPosition = GerenciadorSalve.inst.StringParaVector(valores[1]);
	}


}
