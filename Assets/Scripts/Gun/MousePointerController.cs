using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerController : MonoBehaviour {

    private GameObject _Cursor;

	private void Awake ()
    {
        Cursor.visible = false;

    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; 
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
