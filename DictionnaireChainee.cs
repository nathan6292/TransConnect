using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class DictionnaireChainee<T,U>
{
    private NoeudDico<T,U> tete;

    public DictionnaireChainee(NoeudDico<T, U> element1, NoeudDico<T, U> element2 = null, NoeudDico<T, U> element3 = null, NoeudDico<T,U> element4 = null)
    {
        tete = element1;
        if (element2!=null) tete.Suivant = element2;
        if (element3!=null) element2.Suivant = element3;
        if (element4!=null) element3.Suivant = element4;
    }

    public override string ToString()
    {
        string text = "";
        ForEach((T) => text+= "Key : " + T.ToString() + "\nValue : " + Rechercher(T).ToString() + "\n\n\n");
        return text; 
    }

    public void Ajouter(NoeudDico<T,U> element)
    {
        NoeudDico<T,U> temp = tete;
        while (temp.Suivant != null)
        {
            temp = temp.Suivant;
        }
        temp.Suivant = element;
    }

    public void Permuter(T key1, T key2)
    {
        NoeudDico<T, U> previous1 = null;
        NoeudDico<T, U> previous2 = null;
        NoeudDico<T, U> current1 = tete;
        NoeudDico<T, U> current2 = tete;

        while (current1 != null && !current1.Key.Equals(key1))
        {
            previous1 = current1;
            current1 = current1.Suivant;
        }

        while (current2 != null && !current2.Key.Equals(key2))
        {
            previous2 = current2;
            current2 = current2.Suivant;
        }

        // Si un des éléments est en tête
        if (previous1 == null)
        {
            tete = current2;
        }
        else
        {
            previous1.Suivant = current2;
        }

        if (previous2 == null)
        {
            tete = current1;
        }
        else
        {
            previous2.Suivant = current1;
        }

        // Permutation des suivants
        NoeudDico<T, U> temp = current1.Suivant;
        current1.Suivant = current2.Suivant;
        current2.Suivant = temp;
    }


    public void Supprimer(T key)
    {
        NoeudDico<T,U> temp = tete;
        NoeudDico<T,U> temp2 = tete;
        while (temp.Key.Equals(key) == false)
        {
            temp2 = temp;
            temp = temp.Suivant;
        }
        temp2.Suivant = temp.Suivant;
    }

    public U Rechercher(T key)
    {
        NoeudDico<T,U> temp = tete;
        while (temp.Key.Equals(key) == false)
        {
            temp = temp.Suivant;
        }
        return temp.Value;
    }

    public DictionnaireChainee<T,U> FindAll(Predicate<T> match)
    {
        DictionnaireChainee<T,U> results = new DictionnaireChainee<T,U>(null);

        NoeudDico<T, U> current = tete;

        while (current != null)
        {
            if (match(current.Key))
            {
                if (results.tete == null)
                {
                    results.tete = new NoeudDico<T, U>(current.Key, current.Value);
                }
                else
                {
                    results.Ajouter(new NoeudDico<T, U>(current.Key, current.Value));
                }
            }
            current = current.Suivant;
        }

        return results;
    }



    public void ForEach(Action<T> action)
    {
        NoeudDico<T, U> current = tete;

        while (current != null)
        {
            action(current.Key);
            current = current.Suivant;
        }
    }

}
}