using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid_Inimigo : MonoBehaviour {

  public float grid = 1;
    private float x = 0;
    private float y = 0;
 
    public float vida;

    public Image imgVida;


    void Awake () {

        vida = 1;
        
        if (grid > 0)
        {
            float recalc = grid / grid;
            
            x = Mathf.Round(transform.position.x * recalc) / recalc;
            y = Mathf.Round(transform.position.z * recalc) / recalc;

            transform.position = new Vector3(x - 1, transform.position.y, y + 8);
        }  
	}
	


    public void RecebeDanos()
    {
        vida -= 0.01f;
        imgVida.fillAmount = vida;

        if(vida <= 0)
        {
            Gamemanager.inst.constInimiga.Remove(gameObject);
            Destroy(gameObject);
        }
    }
 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(transform.position.x,transform.position.y+ 0.2f,transform.position.z), new Vector3(2.1f,0.2f,2.1f));
        
    }



}
