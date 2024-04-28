
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
        buttons[3].onClick.AddListener(() => PlaySong(3));
        buttons[4].onClick.AddListener(() => PlaySong(4));
        buttons[5].onClick.AddListener(() => PlaySong(5));
        buttons[6].onClick.AddListener(() => PlaySong(6));
        buttons[7].onClick.AddListener(() => PlaySong(7));
        buttons[8].onClick.AddListener(() => PlaySong(8));
        buttons[9].onClick.AddListener(() => PlaySong(9));
        buttons[10].onClick.AddListener(() => PlaySong(10));
        buttons[11].onClick.AddListener(() => PlaySong(11));

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
            MusicManager.Instance.ToggleMute();
            icons[1].SetActive(false);
            icons[0].SetActive(true);

        }
       
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
