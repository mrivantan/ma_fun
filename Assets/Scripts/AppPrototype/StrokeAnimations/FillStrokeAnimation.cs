using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class FillStrokeAnimation : MonoBehaviour {

    public enum FillType { LeftToRight, TopToBottm};
    public FillType fillType;
    public float duration;
    
    private RectTransform maskTransform;
    private SpriteRenderer sprite;
    private Vector2 mask;

    private const float PixelToUnit = 100f;
    private float speed;
    private Vector3 target;
    private Vector3 origin;
    private UnityAction Animate;

    private void Start()
    {
        maskTransform = GetComponentInParent<RectTransform>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        mask = new Vector2( maskTransform.rect.width, maskTransform.rect.height);
        switch (fillType)
        {
            case FillType.LeftToRight:
                origin = new Vector3(mask.x * -0.5f, 0f, 0f);
                speed = mask.x / PixelToUnit / duration;
                target = new Vector3(mask.x * 0.5f, 0f, 0f);
                break;
            case FillType.TopToBottm:
                origin = new Vector3(0f, mask.y * 0.5f, 0f);
                speed = mask.y / PixelToUnit / duration;
                target = new Vector3(0f, mask.y * -0.5f, 0f);
                break;
        }

    }

    private void Update()
    {
        if(Animate != null)
            Animate();
    }

    public void Trigger()
    {
        InitializePosition();
        switch (fillType)
        {
            case FillType.LeftToRight:
                Animate += LeftToRightAnimation;
                break;
            case FillType.TopToBottm:
                Animate += TopToBottomAnimation;
                break;
        }
    }

    public void Reset()
    {
        InitializePosition();
    }


    private void LeftToRightAnimation()
    {
        transform.Translate(Vector2.right * Time.smoothDeltaTime * speed, maskTransform);
        if(transform.localPosition.x >= target.x)
        {
            transform.localPosition = target;
            Animate -= LeftToRightAnimation;
        }
    }

    private void TopToBottomAnimation()
    {
        transform.Translate(Vector2.down * Time.smoothDeltaTime * speed, maskTransform);
        if (transform.localPosition.y <= target.y)
        {
            transform.localPosition = target;
            Animate -= TopToBottomAnimation;
        }
    }

    private void InitializePosition()
    {
        transform.localScale = new Vector3(mask.x, mask.y, 1f);
        transform.localPosition = origin;
    }
}
