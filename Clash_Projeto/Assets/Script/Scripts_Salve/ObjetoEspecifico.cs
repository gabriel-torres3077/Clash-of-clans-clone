using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoEspecifico : ClasseSalve {

	public bool construcaoFim;
	public Grid constFinalizada;

	
	// Update is called once per frame
	void Update () {
		

	}

	public override void Salvar(int id)
	{
		base.Salvar(id);

	}

	public override void Carrega(string[] valores)
	{
		base.Carrega(valores);
		constFinalizada.DesligaSelecao();
	}
}
