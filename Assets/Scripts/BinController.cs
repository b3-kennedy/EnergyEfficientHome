
using UnityEngine;

public class BinController : MonoBehaviour
{
    public bool isRecycleBin;


    public event System.Action<GameObject> OnCorrect;

   

    

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        bool correct = (other.gameObject.GetComponent<RecycleItem>().isRecyclable == isRecycleBin);

        if (correct)
        {
            

            OnCorrect?.Invoke(other.gameObject);
            other.gameObject.GetComponent<RecycleItem>().isDroppedCorrect = true;


        }
       
    }

    

}
