using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class BugControl : MonoBehaviour
{
    public NavMeshAgent bug;
    public InsectStats stats;
    public bool isParent;
    public Transform ChildBug;
    public MeadowManager meadow;
    public AudioSource BugEat;

    private bool hasTarget;
    private bool reachedTarget;
    private bool isEating;
    private List<GameObject> targets;
    private GameObject target;

    private GameObject PlantInReach;
    private PlantLife PlantLife;

    private void Awake()
    {
        meadow = GameObject.Find("MeadowManager").GetComponent<MeadowManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        meadow.Bugs++;
        hasTarget = false;
        reachedTarget = false;
        isEating = false;
        targets = LookForFlowers();
        stats.CollectedPollen = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTarget)
        {
            SetTarget();
        }

        if (hasTarget)
        {
            if (!target.activeSelf)
            {
                hasTarget = false;
                SetTarget();
            }

            if (reachedTarget && !isEating)
            {
                StartCoroutine(EatFlower());
                isEating = true;
            }
        }

        if (isParent && stats.CollectedPollen == 60)
        {
            StopAllCoroutines();
            stats.CollectedPollen = 0;
            Clone();
            SetTarget();
        }
    }

    private List<GameObject> LookForFlowers()
    {
        return meadow.LivingPlants;
    }

    private void GoToTarget(Transform flower)
    {
        RaycastHit hit;
        if (Physics.Raycast(flower.position + new Vector3(0f, 3f, 0f), new Vector3(0f, -1f, 0f), out hit, Mathf.Infinity, layerMask: 1 << 10))
        {
            bug.SetDestination(hit.point);
        }
    }

    private void SetTarget()
    {
        targets = LookForFlowers();
        target = targets[Random.Range(0, targets.Count)];
        GoToTarget(target.transform);
        hasTarget = true;
        reachedTarget = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            if (other.gameObject == target)
            {
                PlantLife = other.gameObject.GetComponent<PlantLife>();
                reachedTarget = true;
                StartCoroutine(EatFlower());
            }
        }
    }

    private IEnumerator EatFlower()
    {
        if (PlantLife)
        {
            while (PlantLife.Viva > 0)
            {
                stats.CollectedPollen++;
                PlantLife.Viva--;
                yield return new WaitForSeconds(0.21f);
            }

            GameObject.Find("Plane").GetComponent<GridManager>().NewPoint(PlantLife.root);
            meadow.LivingPlants.Remove(target);
            Destroy(target);
            BugEat.Play();
            yield return new WaitForSeconds(0.5f);
            SetTarget();
            yield break;
        }
        else
        {
            yield break;
        }

    }

    private void Clone()
    {
        Instantiate(ChildBug, gameObject.transform.position, Quaternion.identity);
    }

}
