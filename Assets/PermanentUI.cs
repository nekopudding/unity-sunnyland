using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanentUI : MonoBehaviour
{
    public int gems = 0;
    public TextMeshProUGUI gemText;

    public static PermanentUI perm;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        if(!perm) {
            perm = this;
        }
        else
            Destroy(gameObject);
    }
    public void Reset(){
        gems = 0;
        gemText.text = gems.ToString();
    }
}
