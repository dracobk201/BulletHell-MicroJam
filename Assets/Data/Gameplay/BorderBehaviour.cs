using UnityEngine;
using ScriptableObjectArchitecture;

public class BorderBehaviour : MonoBehaviour
{
    [SerializeField] private BoolCollection directionLock = default(BoolCollection);
    [SerializeField] private BorderSide borderSide = default(BorderSide);

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.PlayerTag))
            LockUnlockDirection(borderSide, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string targetTag = collision.tag;
        if (targetTag.Equals(Global.PlayerTag))
            LockUnlockDirection(borderSide, false);
    }

    private void LockUnlockDirection(BorderSide borderSide, bool enterToLock) 
    {
        switch (borderSide)
        {
            case BorderSide.Left:
                directionLock[0] = enterToLock;
                break;
            case BorderSide.Right:
                directionLock[1] = enterToLock;
                break;
            case BorderSide.Up:
                directionLock[2] = enterToLock;
                break;
            case BorderSide.Down:
                directionLock[3] = enterToLock;
                break;
        }
    }
}

public enum BorderSide { Left, Right, Up, Down }
