using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float waitTimeBtwNote;        
    [SerializeField] private float Timer;        
    [SerializeField] private List<int> spawnCount;
    [SerializeField] private float waitTime;
    [SerializeField] private TextMeshPro UITextMeshPro;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI UICombo;
    [SerializeField] private TextMeshProUGUI UIScore;
    private bool couStart;
    //need object to spawn

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = shuffleList(spawnCount);
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        UITextMeshPro.text = Timer.ToString();
        if (Timer > 0)
        {
            if (couStart == false)
            {
                StartCoroutine(spawnNote());
            }
                   
        }
        else
        {
            UICombo.text = ScoreManager.Instance.getCombo().ToString();
            UIScore.text = ScoreManager.Instance.getScore().ToString();
            canvas.SetActive(true);

        }
       
    }

   IEnumerator spawnNote()
    {
        couStart = true;
        for (int i = 0; i < spawnCount.Count; i++)
        {
            if(Timer <= 0) { break; }
            for (int j = 0; j < spawnCount[i]; j++)
            {
                GameObject note = ObjectPoolEX.SharedInstance.GetPooledObject();
                if (note != null)
                {
                    note.transform.position = this.transform.position;
                    note.SetActive(true);
                    yield return new WaitForSeconds(waitTime);
                }
            }
            
            yield return new WaitForSeconds(waitTimeBtwNote);
        }
        spawnCount = shuffleList(spawnCount);
        couStart = false;
    }

    private List<int> shuffleList(List<int> list)
    {
        List<int> result = new List<int>();

        result = list.OrderBy(x => Random.value).ToList();
        return result;

    }
}
