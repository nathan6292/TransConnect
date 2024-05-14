using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class NoeudDico<T,U> : IsToString
{
    private T key;
    private U value;
    private NoeudDico<T,U> suivant;

        #region Accesseurs
        /// <summary>
        /// Accesseur de la clé en lecture et écriture
        /// </summary>
        public T Key
    {
        get { return key; }
        set { key = value; }
    }

        /// <summary>
        /// Accesseur de la valeur en lecture et écriture
        /// </summary>
        public U Value
    {
        get { return value; }
        set { this.value = value; }
    }

        /// <summary>
        ///        /// Accesseur du noeud suivant en lecture et écriture
        ///               /// </summary>
        public NoeudDico<T,U> Suivant
    {
        get { return suivant; }
        set { suivant = value; }
    }

        #endregion

        /// <summary>
        /// Constructeur de la classe NoeudDico
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="suivant"></param>
        public NoeudDico(T key, U value, NoeudDico<T,U> suivant = null)
    {
        this.key = key;
        this.value = value;
        this.suivant = suivant;
    }
        /// <summary>
        /// Affichage de la clé et de la valeur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return key.ToString() + " : " + value.ToString();
    }
}
}