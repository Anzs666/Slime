using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Character
{
    public class CharacterSelector : MonoBehaviour
    {
        private GameObject selectedGO;
        public string selectedName = "selected";
        private float hideTime;

        // Start is called before the first frame update
        void Start()
        {
            selectedGO = transform.Find(selectedName).gameObject;
        
        }

        private void SetSelectedActive(bool state)
        {
            selectedGO.SetActive(state);
            if (state)
            {
                hideTime = Time.time + 3;
            }
        }

        private void Update()
        {
          
        }
    }
}