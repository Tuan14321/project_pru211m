using UnityEngine;

public class MoveMobile : MonoBehaviour
{
    private int auxDirecao;
    private float speed;
    private Animator anim;
    private enum MovementState
    {
        ide, running
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        speed = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        if (auxDirecao != 0)
        {
            transform.Translate(speed * Time.deltaTime * auxDirecao, 0, 0);
        }
        if (auxDirecao < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (auxDirecao > 0)
        {

            GetComponent<SpriteRenderer>().flipX = false;
        }
        anim.SetInteger("state", Mathf.Abs(auxDirecao));
    }
    public void MobileMove(int move)
    {
        auxDirecao = move;
    }
}
