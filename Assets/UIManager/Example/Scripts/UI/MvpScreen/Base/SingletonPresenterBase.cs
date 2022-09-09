using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPresenterBase<T> : PresenterBase<T>
    where T : Model
{
    private SingletonPresenterBase<T> _instance;

    public SingletonPresenterBase<T> Instance
    {
        get { return _instance ?? (_instance = new SingletonPresenterBase<T>()); }
    }
}
