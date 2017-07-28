using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CharacterLoadoutMenu : MonoBehaviour {

    [SerializeField]
    private List<GameObject> loadoutLineup;
    [SerializeField]
    private Text gunNameHolder;

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
        setGunName(loadoutLineup[idx].ToString().Replace("(Clone)",""));
    }

    private void Awake()
    {
        DontDestroyOnLoad(GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update () {
        loadoutLineup[idx].transform.Rotate(0,rotationPace,0);
	}

    private GameObject Next()
    {
        loadoutLineup[idx].SetActive(false);
        idx = (idx + 1) % loadoutLineup.Count;
        loadoutLineup[idx].SetActive(true);

        return loadoutLineup[idx];
    }

    private GameObject Prev()
    {
        loadoutLineup[idx].SetActive(false);
        idx = (--idx < 0) ? (loadoutLineup.Count - 1) : idx;
        loadoutLineup[idx].SetActive(true);

        return loadoutLineup[idx];
    }

    private void setGunName(string _newName)
    {
        gunNameHolder.text = _newName;
    }

    public void traverseRight(bool _toRight)
    {
        GameObject newGun;

        if(_toRight)
        {
            newGun = Next();
        }
        else
        {
            newGun = Prev();
        }

        setGunName(newGun.ToString().Replace("(Clone)", ""));

    }

    public void readyLevel(string _levelName)
    {
        PlayerPrefs.SetInt("playerLoadout",idx);
        SceneManager.LoadScene(_levelName);
    }
}
