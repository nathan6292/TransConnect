using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class NoeudDico<T,U>
{
    private T key;
    private U value;
    private NoeudDico<T,U> suivant;

    #region Accesseurs

    public T Key
    {
        get { return key; }
        set { key = value; }
    }

    public U Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public NoeudDico<T,U> Suivant
    {
        get { return suivant; }
        set { suivant = value; }
    }

    #endregion
    public NoeudDico(T key, U value, NoeudDico<T,U> suivant = null)
    {
        this.key = key;
        this.value = value;
        this.suivant = suivant;
    }

    public override string ToString()
    {
        return key.ToString() + " : " + value.ToString();
    }
}
}