using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _startDuration;

    [SerializeField]
    private float _endDuration;

    [SerializeField]
    private House _house;

    [SerializeField]
    private GameObject _startView1;

    [SerializeField]
    private GameObject _startView2;

    private void Start()
    {
        PhysicsCompositeRoot.Records.GameOverred += () =>
         StartCoroutine(OnGameOverRoutine());
        StartCoroutine(StartPointAnimation());
        _house.GameEnded += () =>
        StartCoroutine(OnWonRoutine());
    }

    private IEnumerator StartPointAnimation()
    {
        _startView1.SetActive(false);
        _startView2.SetActive(false);

        PauseManager.Pause();
        yield return new WaitForSeconds(_startDuration/3f);
        _startView1.SetActive(true);
        yield return new WaitForSeconds(_startDuration/3f);
        _startView1.SetActive(false);
        _startView2.SetActive(true);
        yield return new WaitForSeconds(_startDuration/3f);
        _startView2.SetActive(false);
        PauseManager.Unpause();
    }

    private IEnumerator OnGameOverRoutine()
    {
        PauseManager.Pause();
        yield return new WaitForSeconds(_endDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator OnWonRoutine()
    {
        PauseManager.Pause();
        yield return new WaitForSeconds(_endDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
