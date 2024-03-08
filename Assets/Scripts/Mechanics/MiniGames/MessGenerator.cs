using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessGenerator : MonoBehaviour
{
    private List<TargetTap> messes = new List<TargetTap>();
    private int activeMess;
   

    private void Awake()
    {
        int iterate = 0;
        foreach(Transform child in gameObject.transform)
        {

            messes.Add( child.gameObject.GetComponent<TargetTap>());
           child.gameObject.SetActive(false);
            iterate++;

        }

         //CreateMess();

       // print(messes[0].gameObject.activeInHierarchy);
    }

   public void CreateRandomMess()
    {
        int randomIndex = Random.Range(0, messes.Count - 1);
        do
        {
           randomIndex = Random.Range(0, messes.Count - 1);
        } while (messes[randomIndex].gameObject.activeInHierarchy);

        messes[randomIndex].gameObject.SetActive(true);
    }

    public void CreateTargetMess()
    {

    }
}
