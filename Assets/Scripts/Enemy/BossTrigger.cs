using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.instance.healthBoss.SetActive(true);
            UIManager.instance.camTargetGroup.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
