using System.Collections;
using UnityEngine;

public class TrainMovementController : MonoBehaviour
{
    #region Movement

    [Header("Movement")]
    [Tooltip("Higher == slower")]
    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float rotationSpeed = 3f;

    [SerializeField]
    private Transform spinCentre = null;

    #endregion

    #region Doors

    [Header("Doors")]
    [SerializeField]
    private TrainDoorController controller = null;

    [SerializeField]
    private float timeBeforeDoorOpen = 2;
    [SerializeField]
    private float timeAfterDoorClose = 2;

    private float transition = 0;

    private float multiply = 1;

    public int CurrentNodeIndex { get; set; }

    private bool isStopped;

    public Coroutine stationStopped = null;

    private bool needToStop = true;

    #endregion

    #region Interaction Button

    public enum ButtonColor
    {
        Red,
        Green,
        Yellow
    }

    [Header("Interaction Button")]
    [SerializeField]
    private Material[] colors = null;

    [SerializeField]
    private MeshRenderer interactionBtnRenderer = null;

    [SerializeField]
    private float timeFlickering = 0.2f;

    private Coroutine flickering = null;

    #endregion

    #region Sounds

    [Header("SFX")]
    [SerializeField]
    private AudioSource idleSource = null;

    [SerializeField]
    private AudioSource anotherIdleSource = null;

    [SerializeField]
    private AudioSource oneShotSource = null;

    #endregion

    private Material GetMaterialFromColor(ButtonColor btnColor)
    {
        return colors[(int)btnColor];
    }

    protected void Start()
    {
        StopCheck();
    }

    protected void Update()
    {
        if (!isStopped)
        {
            Move();
        }
    }

    public void StopBtn()
    {
        needToStop = true;
        if (flickering == null)
        {
            anotherIdleSource.Play();
            flickering = StartCoroutine(ColorFlicker());
        }

    }

    private IEnumerator ColorFlicker()
    {
        while (true)
        {
            interactionBtnRenderer.material = GetMaterialFromColor(ButtonColor.Yellow);
            yield return new WaitForSeconds(timeFlickering);
            interactionBtnRenderer.material = GetMaterialFromColor(ButtonColor.Red);
            yield return new WaitForSeconds(timeFlickering);
        }

    }

    private void Move()
    {
        if (!idleSource.isPlaying)
        {
            idleSource.Play();
        }

        transition += (Time.deltaTime / speed) * multiply;

        if (transition > 1)
        {
            transition = 0;

            StopCheck();
            CurrentNodeIndex++;

            if (CurrentNodeIndex == Path.Instance.GetNodesCount() - 1)
            {
                Reverse(true);
                return;
            }
        }

        else if (transition < 0)
        {
            transition = 1;

            StopCheck();
            CurrentNodeIndex--;
            if (CurrentNodeIndex == -1)
            {
                Reverse(false);
                return;
            }
        }

        if (stationStopped != null)
        {
            oneShotSource.Play();
            return;
        }

        transform.position = Path.Instance.GetLinearPositionFromNode(CurrentNodeIndex, transition);

        Vector3 forward = Path.Instance.GetTransformDestination(CurrentNodeIndex).position - this.transform.position;
        if (forward != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

    }

    private void Reverse(bool reverse)
    {
        Path.Instance.ReverseNodes(reverse);
        multiply *= -1;

        Vector3 rotTrain = spinCentre.eulerAngles;
        rotTrain = new Vector3(rotTrain.x, rotTrain.y + 180 * multiply, rotTrain.z);
        spinCentre.rotation = Quaternion.Euler(rotTrain);
    }

    private void StopCheck()
    {
        if (needToStop && stationStopped == null)
        {
            StopAllCoroutines();
            controller.MainCollider.enabled = true;
            stationStopped = StartCoroutine(StationStop());
        }
    }

    public IEnumerator StationStop(float multiplier = 1)
    {
        needToStop = false;
        flickering = null;
        anotherIdleSource.Stop();
        idleSource.Pause();
        interactionBtnRenderer.material = GetMaterialFromColor(ButtonColor.Green);
        isStopped = true;

        yield return new WaitForSeconds(timeBeforeDoorOpen);

        yield return controller.Open(multiplier);
        yield return controller.Close();

        yield return new WaitForSeconds(timeAfterDoorClose);

        interactionBtnRenderer.material = GetMaterialFromColor(ButtonColor.Red);
        isStopped = false;
        stationStopped = null;
        controller.MainCollider.enabled = false;
        idleSource.UnPause();
    }
}
