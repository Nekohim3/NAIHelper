using System;
using NAIHelper.Models;

namespace NAIHelper.ViewModels.UI_Entities;

//public abstract class UI_Wrapper<T> : ViewModelBase where T : IdEntity, new()
//{
//    public T E { get; private set; }

//    protected UI_Wrapper()
//    {
//        E = new T();
//    }

//    protected UI_Wrapper(T e)
//    {
//        if (e == null)
//            throw new ArgumentNullException("t null");
//        E = e;
//    }

//    public virtual T GetEntity()
//    {
//        return E;
//    }

//    public override int GetHashCode() => E.GetHashCode();

//    public bool HasChanges() => E.HasChanges();
//}