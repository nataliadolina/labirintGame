using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ChestBase : MonoBehaviour
{
    private Animator animator;
    [SerializeField]  private float animLength;
    private bool setTransp;
    [SerializeField] GameObject objWithRenderer;
    [SerializeField] GameObject model;
    [SerializeField] float step;

    private Button button;
    private Renderer renderer;
    protected void Init()
    {
        renderer = objWithRenderer.GetComponent<Renderer>();
        animator = model.GetComponent<Animator>();
        button = Buttons.instance.openButton;
    }
    protected void Open()
    {
        animator.SetTrigger("open");
        StartCoroutine("WaitToOpen");
    }
    protected void UpdateFunc()
    {
        MakeTransparent();
    }
    protected void MakeTransparent()
    {
        if (setTransp)
        {
            Color _color = renderer.material.color;
            // уменьшаем прозрачность
            if (_color.a > 0.05f)
            {
                _color.a = _color.a - step / 1000;
                renderer.material.color = _color;
            }
            else
            {
                setTransp = false;
                Destroy(gameObject);
            }
        }
    }
    protected IEnumerable WaitToOpen()
    {
        yield return new WaitForSeconds(animLength);
        setTransp = true;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.interactable = true;
            Buttons.instance.Open += Open;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.interactable = false;
            Buttons.instance.Open -= Open;
        }
    }

}
