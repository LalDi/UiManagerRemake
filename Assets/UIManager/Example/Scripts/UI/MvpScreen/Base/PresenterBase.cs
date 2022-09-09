using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenterBase<T> : MonoBehaviour
    where T : Model
{
    public T model;
}
