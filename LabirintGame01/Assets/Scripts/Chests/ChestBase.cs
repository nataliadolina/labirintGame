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

    protected IOpenBehaviour openBehaviour;

    public delegate void InvokeEnemies(Vector3 targetPos);
    public static InvokeEnemies InvokeEnemy;

    protected virtual void Start()
    {
        renderer = objWithRenderer.GetComponent<Renderer>();
        animator = model.GetComponent<Animator>();
        button = Buttons.instance.openButton;
    }
    private void Open()
    {
        InvokeEnemy(transform.position);
        animator.SetTrigger("open");
        StartCoroutine("WaitToOpen");
    }
    protected virtual void Update()
    {
        MakeTransparent();
    }
    private void MakeTransparent()
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
    private IEnumerable WaitToOpen()
    {
        yield return new WaitForSeconds(animLength);
        Debug.Log("Coroutine started");
        setTransp = true;
        
        Debug.Log("Invoked");
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.interactable = true;
            Buttons.instance.Open += Open;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.interactable = false;
            Buttons.instance.Open -= Open;
        }
    }

}
