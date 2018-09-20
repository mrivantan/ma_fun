using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace App.Model.Word
{
    [RequireComponent(typeof(WordModelManager))]
    public class WordModelController : MonoBehaviour
    {

        public UnityAction InitializeModel;

        private WordModelManager manager;
        void Start()
        {
            manager = GetComponent<WordModelManager>();
            InitializeModel += manager.PopulateModel;
            InitializeModel += manager.LoadWordData;

            InitializeModel();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}