using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CriaGuerreiros_cena : MonoBehaviour {

	   public GameObject prefab;
	   public Text txtNum;

	   public bool liberaCriacao;

	   public Button button;
	   
	void Start()
	{
		liberaCriacao = false;
		txtNum.text = Gamemanager.inst.guerreirosNum.ToString();
	}
      void Update()
      {
		  if(liberaCriacao)
		  {
            if (Input.GetMouseButtonDown(0) && Gamemanager.inst.guerreirosNum > 0 && !SobreUI()) {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000)) {
                    Instantiate(prefab,hit.point,Quaternion.identity);
					Gamemanager.inst.guerreirosNum--;
					txtNum.text = Gamemanager.inst.guerreirosNum.ToString();
					print("criou");
                }
            }
		  }

      }

	   bool SobreUI()
	  {
		  return EventSystem.current.IsPointerOverGameObject();
	  }

	  public void Liberacao()
	  {
		  liberaCriacao = true;
		  button.image.color = Color.green;
	  }

}


