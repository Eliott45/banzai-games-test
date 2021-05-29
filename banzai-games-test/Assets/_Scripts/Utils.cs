using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class Utils : MonoBehaviour
    {
        /// <summary>
        /// Возвращает сприсок всех материалов в данном игровом объекте и его дочерних объектов.
        /// </summary>
        public static Material[] GetAllMaterials(GameObject go) {
            var rends = go.GetComponentsInChildren<Renderer>();
            return(rends.Select(rend => rend.material).ToArray());
        }
    }
}
