using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter Thiscounter;
    [SerializeField] private GameObject counterVis;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == Thiscounter)
        {
            ShowVis();
        }
        else
        {
            UnshowVis();
        }
    }

    private void ShowVis()
    {
        counterVis.SetActive(true);
    }

    private void UnshowVis()
    {
        counterVis.SetActive(false);
    }
}
