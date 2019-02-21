using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	void Start () {

        Debug.Log("Load data player (A priori): " + PlayerData.Instance.FirstTime);

        Debug.Log("Save data player: ");
        PlayerData.Instance.FirstTime = true;

        Debug.Log("Load data player (A posteriori): " + PlayerData.Instance.FirstTime);
    }
}
