using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Animator _animator;
    [SerializeField] LoadSprites _loadSprites;
    public void OnPointerClick(PointerEventData eventData) {
        _animator.SetBool("anim", true);
        _loadSprites.Load();
    }
}
