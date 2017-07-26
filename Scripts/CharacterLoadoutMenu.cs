using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterLoadoutMenu : MonoBehaviour {

    [SerializeField]
    private List<GameObject> loadoutLineup;

    private Transform placeHolder;
    private int idx;
    public float rotationPace;

	// Use this for initialization
	void Start () {
        placeHolder = GetComponent<Transform>();

        GameObject[] loadouts = Resources.LoadAll<GameObject>("Loadouts");

        // Throw error if loadouts don't exist
        if (loadouts.Length == 0)
        {
            Debug.LogError("No loadout exists to load. Check /Loadouts");
        }

		foreach(GameObject loadout in loadouts)
        {
            GameObject _loadout = Instantiate(loadout);
            _loadout.transform.SetParent(placeHolder);
            _loadout.transform.position = placeHolder.position;

            loadoutLineup.Add(_loadout);
            _loadout.SetActive(false);
        }
        loadoutLineup[idx].SetActive(true);
    }

    private void Awake()
    {
        DontDestroyOnLoad(GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update () {
        loadoutLineup[idx].transform.Rotate(0,rotationPace,0);
	}

    public void Next()
    {
        loadoutLineup[idx].SetActive(false);
        idx = (idx + 1) % loadoutLineup.Count;
        loadoutLineup[idx].SetActive(true);
    }

    public void Prev()
    {
        loadoutLineup[idx].SetActive(false);
        idx = (--idx < 0) ? (loadoutLineup.Count - 1) : idx;
        loadoutLineup[idx].SetActive(true);
    }

    public void readyLevel(string _levelName)
    {
        PlayerPrefs.SetInt("playerLoadout",idx);
        SceneManager.LoadScene(_levelName);
    }
}
