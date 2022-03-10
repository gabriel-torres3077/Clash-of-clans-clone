using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Grid : MonoBehaviour {

    public float grid = 1;
    private float x = 0;
    private float y = 0;
    private Vector3 screenpoint;
    private Plane movePlane;

    [SerializeField]
    private float fixDist = 0.5f;
    private float hitDist;
    private float calc;
    private Ray camRay;
    private Vector3 point, cordPoint;


    private float rot;
    private Renderer render;

   

   
    [SerializeField]
    private GameObject canvas;
    public GameObject canvasEvolucao;
    private static Transform trSelect = null; 
    public bool selected = false;

    public MeshRenderer mr;

    [SerializeField]
    private Texture[] tex;

    public Vector3 posInicial;

    private bool liberaObjCena;

    public LayerMask m_LayerMask;
    public BoxCollider colisorCaixa;

    public bool colisao;

    public GameObject painelCriacaoObj;

    [SerializeField]
    private float tempoInicio,tempo;
    public Image barraTempo;
    public GameObject uiTempo;
    public bool construcaoFinalizada;


    void Start () {

      if(construcaoFinalizada)
      {
        GerenciadorSalve.inst.salveObjs.Add(this.GetComponent<ObjetoEspecifico>());
        GerenciadorSalve.inst.Salvar();
      }
        
        if(canvasEvolucao != null)
        {
            canvasEvolucao.SetActive(false);
        }

        
        
        mr.material.SetTexture("_MainTex",tex[0]);

        rot = transform.rotation.y;
        render = GetComponent<Renderer>();        
        canvas.SetActive(false);


        if(Gamemanager.inst.nascimento)
        {
            painelCriacaoObj.SetActive(true);
            selected = true;
            trSelect = transform;
            mr.enabled = true;
            canvas.SetActive(true);
            colisorCaixa.enabled = false;
            liberaObjCena = false;
            Gamemanager.inst.nascimento = false;
        }

        tempo = tempoInicio;

	}
	
	void Update () 
    {

        

        if(uiTempo.activeSelf && !colisao)
        {
            if(construcaoFinalizada)
            {
                uiTempo.SetActive(false);
                
            }

            else if(tempo > 0)
            {
                tempo -= Time.deltaTime;
                barraTempo.fillAmount = (tempo/tempoInicio);
            }
            else
            {
                uiTempo.SetActive(false);
                construcaoFinalizada = true;
            }

            if(construcaoFinalizada)
            {
                uiTempo.SetActive(false);
                painelCriacaoObj.SetActive(false);

            }
            
        }



        if (grid > 0)
        {
            float recalc = grid / grid;
            
            x = Mathf.Round(transform.position.x * recalc) / recalc;
            y = Mathf.Round(transform.position.z * recalc) / recalc;

            transform.position = new Vector3(x, transform.position.y, y);
        }

        if(selected == true && transform != trSelect) 
        {
            selected = false;
            canvas.SetActive(false);
        }

        if(selected == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(new Vector3(0, rot - 90, 0));
            }
        }

        AjusteAlturaObjSelecionado();
        RetiraSelecaoClick();
        
        
	}

    void ControleDeSelecao()
    {
        if(!uiTempo.activeSelf )
        {
       
            if(selected)
            {
                Gamemanager.inst.itemSelecionado = true;
            }
            else
            {
                Gamemanager.inst.itemSelecionado = false;
            }
        }
    }


    void FixedUpdate()
    {
        MinhasColisoes();
    }

    void AjusteAlturaObjSelecionado()
    {
        if(!uiTempo.activeSelf)
        {
            if(selected)
            {
                transform.position = new Vector3(transform.position.x,0.02f,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x,0.01f,transform.position.z);
            }
        }
    }



    void OnMouseDown()
    {  
        if(!uiTempo.activeSelf)
        {
            movePlane = new Plane(Camera.main.transform.forward.normalized,transform.position);
            selected = true;
            colisorCaixa.enabled = false;
            trSelect = transform;
            canvas.SetActive(true); 
            
            Gamemanager.inst.itemSelecionado = true; 
            if(construcaoFinalizada && canvasEvolucao != null)
            {
                canvasEvolucao.SetActive(true);
            }
        }      
    }


    void OnMouseDrag()
    {
        if(!uiTempo.activeSelf)
        {

            if(selected)
            {     
                  

                    camRay = Camera.main.ScreenPointToRay(Input.mousePosition); 

                    if (movePlane.Raycast(camRay, out hitDist))
                    { 
                        point = camRay.GetPoint(hitDist); 

                        calc = (fixDist - camRay.origin.y) / (camRay.origin.y - point.y);

                        cordPoint.x = camRay.origin.x + (point.x - camRay.origin.x) * -calc;
                        cordPoint.y = camRay.origin.y + (point.y - camRay.origin.y) * -calc;
                        cordPoint.z = camRay.origin.z + (point.z - camRay.origin.z) * -calc;
                        
                    
                        if(Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y") != 0){
                            transform.position = cordPoint;
                        }

                    }
                
                
            }

        }
       
    }



    void MinhasColisoes()
    {
     
            if(selected)
            {
                colisao = Physics.CheckBox(new Vector3(transform.position.x,transform.position.y+ 0.2f,transform.position.z), new Vector3(1.5f,0.2f,1.5f), Quaternion.identity,m_LayerMask);
                if(colisao)
                {
                    mr.material.SetTexture("_MainTex",tex[1]);   
                }
                else
                {
                    mr.material.SetTexture("_MainTex",tex[0]);  
                }
            }
            else
            {
                mr.material.SetTexture("_MainTex",tex[2]);  
            } 
        
        
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(transform.position.x,transform.position.y+ 0.2f,transform.position.z), new Vector3(2.1f,0.2f,2.1f));
        
    }

    public void MovendoZ(string dir)
    {
        if(liberaObjCena)
        {
            if(dir == "baixo")
            {
                cordPoint.z = transform.position.z - 1;
            }
            else if(dir == "cima")
            {
                cordPoint.z = transform.position.z + 1;
            }        
            cordPoint.x = transform.position.x;
            cordPoint.y = transform.position.y;

            transform.position = cordPoint;
        }

        
    }

    public void MovendoX(string dir)
    {

            if(dir == "baixo")
            {
                cordPoint.x = transform.position.x - 1;
            }
            else if(dir == "cima")
            {
                cordPoint.x = transform.position.x + 1;
            }        
            cordPoint.z = transform.position.z;
            cordPoint.y = transform.position.y;

            transform.position = cordPoint;
        

        
    }

private void RetiraSelecaoClick()
{
    if(liberaObjCena)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray rayVar = Camera.main.ScreenPointToRay(Input.mousePosition);
             
                if (Physics.Raycast(rayVar, out hit)) {
                    if (hit.transform.CompareTag("fora"))
                    {
                        DesligaSelecao();  
                        Gamemanager.inst.itemSelecionado = false;  
                        GerenciadorSalve.inst.Salvar();                    
                    }
                }
            }  
        }
    }
    else
    {
        if (Input.GetMouseButtonDown(0) && !SobreUI())
        {
            RaycastHit hit;
            Ray rayVar = Camera.main.ScreenPointToRay(Input.mousePosition);
             
            if (Physics.Raycast(rayVar, out hit)) {
                if (hit.transform.CompareTag("fora"))
                {
                    DeletaNovoObj(gameObject);
                    Gamemanager.inst.itemSelecionado = false;                          
                }
            }
        } 
       
    }

}

bool SobreUI()
{
    return EventSystem.current.IsPointerOverGameObject();        
}

public void DesligaSelecao()
{

    if(!liberaObjCena)
    {
        if(colisao)
        {
            return;
        }
        else
        {
            liberaObjCena = true;
        }
        
    }



    if(liberaObjCena)
    {
        if(colisao)
        {
            transform.position = posInicial;
            painelCriacaoObj.SetActive(false);            
            trSelect = null;
            canvas.SetActive(false);
            if(canvasEvolucao != null)
            {
                canvasEvolucao.SetActive(false);
            }
            
            selected = false;
            mr.material.SetTexture("_MainTex",tex[2]);                        
            colisorCaixa.enabled = true;
        }
        else
        {
            posInicial = transform.position;
            painelCriacaoObj.SetActive(false);            
            trSelect = null;
            canvas.SetActive(false);
            if(canvasEvolucao != null)
            {
                canvasEvolucao.SetActive(false);
            }
            selected = false;
            mr.material.SetTexture("_MainTex",tex[2]);                        
            colisorCaixa.enabled = true;
        }

    }


}

    public void DeletaNovoObj(GameObject obj)
    {
        Destroy(obj);
    }

  
    public void MostraTempoUI()
    {
        if(!uiTempo.activeSelf && !colisao)
        {
            uiTempo.SetActive(true);
            GerenciadorSalve.inst.salveObjs.Add(this.GetComponent<ObjetoEspecifico>());
            GerenciadorSalve.inst.Salvar();
        }
        
    }

    public void FinalizaConstru()
    {
        construcaoFinalizada = true;
        
    }

}
