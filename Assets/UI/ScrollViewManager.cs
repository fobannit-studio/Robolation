using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.UI
{
   
    public class ScrollViewManager<T> :MonoBehaviour where T: MonoBehaviour 
    {
        public List<T> elements;
        public  ScrollViewManager ()
        {
            elements = new List<T>();
        }
        public void ClearList()
        {
        
            foreach (var item in elements)
            {
                Destroy(item.gameObject);
            }
            elements.Clear();
        }
        public T GenerateList(GameObject example)
        {
            GameObject button = Instantiate(example);
            button.SetActive(true);
            var element = button.GetComponent<T>();   
            button.transform.SetParent(example.transform.parent, false);
            elements.Add(element);
            return element;
        }



    }
}