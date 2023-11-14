using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackTiling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.CompareTag("Multi Hat"))
        {
            /*if (this.gameObject.name == "Supertile4(Clone)")
            {
                this.transform.GetChild(0).SetParent(this.gameObject.transform.root);
                Destroy(this);
            }
            if (this.gameObject.name == "Supertile3(Clone)")
            {
                this.transform.GetChild(0).SetParent(this.gameObject.transform.root);
                Destroy(this);
            }
            */
            if (this.gameObject.name == "HatSuperTile(Clone)")
            {
                this.transform.GetChild(0).SetParent(this.gameObject.transform.root);
                this.transform.GetChild(1).SetParent(this.gameObject.transform.root);
                Destroy(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
