using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    [SerializeField]
    private InteractionButton interactionButton = null;

    public InteractionButton InteractionButton => interactionButton;

    [SerializeField]
    private GameObject trainObject = null;
    private GameObject instantedTrain = null;

    [SerializeField]
    private Text stationInfoText = null;
    public Text StationInfoText => stationInfoText;

    public void SpawnTrainBtn(int stationNumber)
    {
        instantedTrain = Instantiate(trainObject, Path.Instance.GetPositionFromNode(stationNumber), Quaternion.identity);
        instantedTrain.GetComponent<TrainMovementController>().CurrentNodeIndex = stationNumber;
        instantedTrain.transform.LookAt(Path.Instance.GetTransformDestination(stationNumber));
    }
}
