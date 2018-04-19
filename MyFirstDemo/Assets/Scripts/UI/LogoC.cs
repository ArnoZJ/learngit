using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoC : MonoBehaviour {

    public GameObject logo;
	// Use this for initialization
	void Start () {
        StartCoroutine(Logo());
    }

    IEnumerator Logo()
    {
        yield return new WaitForSeconds(2);
        logo.SetActive(false);
    }
}
