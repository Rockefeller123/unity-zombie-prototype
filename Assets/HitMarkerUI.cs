using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitMarkerUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float displayTime = 0.05f;

    private Coroutine routine;

    private void Awake()
    {
        if (image != null)
            image.enabled = false;
    }

    public void Show()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        image.enabled = true;
        yield return new WaitForSeconds(displayTime);
        image.enabled = false;
    }
}


