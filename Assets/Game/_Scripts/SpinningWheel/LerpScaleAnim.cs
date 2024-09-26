using System.Collections;
using UnityEngine;

public class LerpScaleAnim : MonoBehaviour
{

    [SerializeField] private Vector3 minScale; //escala mínima
    [SerializeField] private Vector3 maxScale; //escala máxima
    [SerializeField] private float scaleAnimDuration = 0.25f; //tempo de duração da transição


    private Vector3 targetScale;
    private bool scaleUp = true; //define se a escala deve aumentar ou diminuir
    Coroutine animCoroutine;

    private RectTransform t;

    [SerializeField] bool play = false;

    void Start()
    {
        t = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (play)
        {
            if (animCoroutine == null)
            {
                //define se a escala deve aumentar ou diminuir
                targetScale = scaleUp ? maxScale : minScale;
                scaleUp = !scaleUp;

                //verifica se a variável à esquerda é nula antes de atribuir um novo valor
                animCoroutine ??= StartCoroutine(Lerp(targetScale, scaleAnimDuration));
                //na pratica, se animCoroutine for nulo ele chama a corrotina e atribui à variável
            }
        }
    }

    public void PlayScaleAnim()
    {
        play = true;
    }

    public void StopScaleAnim()
    {

        play = false;

        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine);
            animCoroutine = null;
            t.localScale = maxScale;
        }
    }

    //cria a animação interpolando a escala
    IEnumerator Lerp(Vector3 target, float duration)
    {

        float elapsedTime = 0; //tempo decorrido
        Vector3 startValue = t.localScale; //ponto de origem

        while (elapsedTime < duration)
        { //se o tempo decorrido for menor que a duração

            //interpolação entre ponto a e b dado uma porcentagem t que vai de 0 a 1
            t.localScale = Vector3.Lerp(startValue, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime; //atualiza o tempo decorrido
            yield return null;
        }

        //garante que a escala no final alcance o tamanho desejado
        t.localScale = target;
        animCoroutine = null;
    }
}