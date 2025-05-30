using UnityEngine;

public class Muter : MonoBehaviour
{
    public void Click()
    {
        Persistence.Instance.ToggleMute();
    }
}
