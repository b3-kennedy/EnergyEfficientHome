
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BinController : MonoBehaviour
{
    public bool isRecycleBin;


    public event System.Action<int> OnCorrect;

   

    

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        bool correct = (other.gameObject.GetComponent<RecycleItem>().isRecyclable == isRecycleBin);

        if (correct)
        {
            
            Debug.Log("good job");

            OnCorrect?.Invoke(other.gameObject.GetComponent<RecycleItem>().positionIndex);
            other.gameObject.GetComponent<RecycleItem>().isDroppedCorrect = true;



        }
       
    }

    

}
