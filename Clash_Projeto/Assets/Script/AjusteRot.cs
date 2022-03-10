using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjusteRot : MonoBehaviour {

    Quaternion rotacao;

	void Awake () {

        rotacao = transform.rotation;

	}

    private void LateUpdate()
    {
        transform.rotation = rotacao;
    }
}
