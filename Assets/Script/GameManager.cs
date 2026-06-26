using Unity.XR.CoreUtils;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public XROrigin xrOrigin;
    public Transform studentSpawn;
    public Transform teacherSpawn;
    public Transform menuSpawn;
    public GameObject roleSelectionPanel;
    public GameObject backButton;
    private void SpawnPlayer(Transform spawnPoint)
    {
        xrOrigin.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );
        roleSelectionPanel.SetActive(false);
        backButton.SetActive(true);
    }
    public void SelectStudent()
    {
        SpawnPlayer(studentSpawn);
    }
    public void SelectTeacher()
    {
        SpawnPlayer(teacherSpawn);
    }
    public void GoBack()
    {
        xrOrigin.transform.SetPositionAndRotation(menuSpawn.position,menuSpawn.rotation);
        roleSelectionPanel.SetActive(true);
        backButton.SetActive(false);
    }
}
