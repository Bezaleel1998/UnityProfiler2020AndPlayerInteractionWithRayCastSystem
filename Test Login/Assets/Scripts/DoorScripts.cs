using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScripts : MonoBehaviour
{

    [SerializeField]
    private bool _isDoorOpened = false;
    [SerializeField]
    private float _doorTimer = 2f;
    private Animator _anim;

    private void Awake()
    {

        _anim = this.GetComponent<Animator>();

    }

    private void Update()
    {

        DoorOpen();

    }

    public void DoorOpenController()
    {

        _isDoorOpened = true;

    }

    IEnumerator DoorOpenTimmer()
    {

        yield return new WaitForSeconds(_doorTimer);
        _isDoorOpened = false;

    }

    void DoorOpen()
    {
        
        if (_isDoorOpened == true)
        {

            _anim.SetBool("_isOpened", true);
            StartCoroutine(DoorOpenTimmer());

        }
        else
        {
            _anim.SetBool("_isOpened", false);
        }

    }

}
