
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource[] songs;

    public Button[] buttons;
    public Button mute;

    public GameObject[] icons;
    public bool isMuted = false;
   
   
    public void OnEnable()
    {
       
            buttons[0].onClick.AddListener(() => PlaySong(0));
        buttons[1].onClick.AddListener(() => PlaySong(1));
        buttons[2].onClick.AddListener(() => PlaySong(2));


        mute.onClick.AddListener(MuteGame);
        isMuted = MusicManager.Instance.isMuted;

        if (isMuted == false)
        {


            icons[1].SetActive(false);
            icons[0].SetActive(true);

        }
        else
        {

            icons[1].SetActive(true);
            icons[0].SetActive(false);
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        mute.onClick.RemoveAllListeners();
    }
    public void PlaySong(int index)
    {
        if(isMuted == true)
        {
            isMuted = false;
            //MusicManager.Instance.isMuted = isMuted;
            icons[0].SetActive(false);
            icons[1].SetActive(true);
        }
        Debug.Log(index);
        MusicManager.Instance.ChangeMusic(index);
       
    }
    public void MuteGame()
    {
        isMuted = !isMuted;

        if(isMuted==false)
        {
           
           
            icons[1].SetActive(false);
            icons[0].SetActive(true);

        }
        else
        {
           
            icons[1].SetActive(true);
            icons[0].SetActive(false);
        }
        MusicManager.Instance.ToggleMute();
       
    }
}
