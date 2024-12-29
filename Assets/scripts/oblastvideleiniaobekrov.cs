using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class oblastvideleiniaobekrov : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, layerMask;
    private Camera _cam;
    private GameObject _cubeselect;
    private RaycastHit _hit;
    public List<GameObject> playerscar;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)&& playerscar.Count>0){
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit agent, 1000f, layer)){
                foreach(var el in playerscar){
                    el.GetComponent<NavMeshAgent>().SetDestination(agent.point);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            foreach(var el in playerscar){
                if(el!=null){
                    el.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            playerscar.Clear();
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                _cubeselect = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        if (_cubeselect)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = _hit.point.x - hitDrag.point.x * -1;
                float zScale = _hit.point.z - hitDrag.point.z;
                if (xScale < 0.0f && zScale < 0.0f)
                {
                    _cubeselect.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (xScale < 0.0f)
                {
                    _cubeselect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (zScale < 0.0f)
                {
                    _cubeselect.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else{
                    _cubeselect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                _cubeselect.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }

        if (Input.GetMouseButtonUp(0) && _cubeselect)
        {
            RaycastHit[] hits = Physics.BoxCastAll(_cubeselect.transform.position, _cubeselect.transform.localScale, Vector3.up, Quaternion.identity, 0, layerMask);
            foreach (var el in hits)
            {
                if(el.collider.CompareTag("Enemy")) continue;
                playerscar.Add(el.transform.gameObject);
                el.transform.GetChild(0).gameObject.SetActive(true);
            }
            Destroy(_cubeselect);
            _cubeselect = null;
        }
    }
}