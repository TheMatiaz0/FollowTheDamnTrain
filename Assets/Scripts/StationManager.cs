using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField]
    private Station stationPrefab = null;

    [SerializeField]
    private Vector3 offsetPosition;

    private Station instantedStation = null;

    protected void Start()
    {
        for (int i = 0; i < Path.Instance.GetNodesCount(); i++)
        {
            Vector3 offsettedPosition = Path.Instance.GetPositionFromNode(i) + offsetPosition;

            instantedStation = Instantiate(stationPrefab, offsettedPosition, Quaternion.identity, this.transform);
            int savedNum = i;
            instantedStation.InteractionButton.OnClick.AddListener(() => instantedStation.SpawnTrainBtn(savedNum));
            instantedStation.StationInfoText.text = string.Format(instantedStation.StationInfoText.text, i);
        }
    }
}
