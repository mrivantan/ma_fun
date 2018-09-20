using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dollar.One;
namespace App.Model.Word { 
    public class WordModel : MonoBehaviour {

        public Dictionary<string, WordMeta> wordDictionary = new Dictionary<string, WordMeta>();
        public _WordMeta currentWord;
    }
}
