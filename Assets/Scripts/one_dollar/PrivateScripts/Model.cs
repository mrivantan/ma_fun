using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Dollar.One {
    public class Model
    {

        public List<Unistroke> positiveList = new List<Unistroke>();
        public List<Unistroke> negativeList = new List<Unistroke>();
        public StrokeClassification strokeClassification = new StrokeClassification();
        public Dictionary<string, StrokeMeta> strokeDictionary;

        public enum StrokeTypeTest { Negative, Positive, All};
        public Model()
        {
            strokeDictionary = new Dictionary<string, StrokeMeta>();

            string[] none = { StrokeType.variant_none };
            string[] shortlong = { StrokeType.variant_short, StrokeType.variant_long };
            string[] straightslanted = { StrokeType.variant_straight, StrokeType.variant_slanted };
            string[] forwardbackward = { StrokeType.variant_forward, StrokeType.variant_backward };
            strokeDictionary.Add(StrokeType.name_H, new StrokeMeta(0, StrokeType.name_H,        "heng ㇐", straightslanted, new string[] { "1st stroke of 大", "1st stroke of 七" }));
            strokeDictionary.Add(StrokeType.name_HG, new StrokeMeta(1, StrokeType.name_HG,      "heng gou ㇖", shortlong, new string[] { "1st stroke of 疋", "1st stroke of 子" }));
            strokeDictionary.Add(StrokeType.name_HP, new StrokeMeta(2, StrokeType.name_HP,      "heng pie ㇇", shortlong, new string[] { "4th stroke of 今", "1st stroke of 又" }));
            strokeDictionary.Add(StrokeType.name_HPWG, new StrokeMeta(3, StrokeType.name_HPWG,  "heng pie wan gou ㇌", none, new string[] { "1st stroke of 阝" }));
            strokeDictionary.Add(StrokeType.name_HZ, new StrokeMeta(4, StrokeType.name_HZ,      "heng zhe ㇕", none, new string[] { "2nd stroke of 四" }));
            strokeDictionary.Add(StrokeType.name_HZT, new StrokeMeta(5, StrokeType.name_HZT,    "heng zhe ti ㇊", none, new string[] { "2nd stroke of 计" }));
            strokeDictionary.Add(StrokeType.name_HZW, new StrokeMeta(6, StrokeType.name_HZW,    "heng zhe wan ㇍", none, new string[] { "5th stroke of 投" }));
            strokeDictionary.Add(StrokeType.name_HZG, new StrokeMeta(7, StrokeType.name_HZG,    "heng zhe gou ㇆", straightslanted, new string[] { "1st stroke of 羽", "1st stroke of 也" }));
            strokeDictionary.Add(StrokeType.name_HZWG, new StrokeMeta(8, StrokeType.name_HZWG,  "heng zhe wan gou ㇠", shortlong, new string[] { "1st stroke of 飞", "2nd stroke of 九" }));
            strokeDictionary.Add(StrokeType.name_HZZ, new StrokeMeta(9, StrokeType.name_HZZ,    "heng zhe zhe ㇅", none, new string[] { "1st stroke of 卍" }));
            strokeDictionary.Add(StrokeType.name_HZZP, new StrokeMeta(10, StrokeType.name_HZZP, "heng zhe zhe pie ㇋", none, new string[] { "1st stroke of 及" }));
            strokeDictionary.Add(StrokeType.name_HZZZ, new StrokeMeta(11, StrokeType.name_HZZZ, "heng zhe zhe zhe ㇎", none, new string[] { "1st stroke of 凸" }));
            strokeDictionary.Add(StrokeType.name_HZZZG, new StrokeMeta(12, StrokeType.name_HZZZG, "heng zhe zhe zhe gou ㇡", none, new string[] { "1st stroke of 乃" }));
            strokeDictionary.Add(StrokeType.name_HXWG, new StrokeMeta(13, StrokeType.name_HXWG, "heng xie wan gou 乙", none, new string[] { "1st stroke of 乙" }));
            strokeDictionary.Add(StrokeType.name_T, new StrokeMeta(14, StrokeType.name_T,       "ti ㇀", none, new string[] { "2nd stroke of 冰" }));
            strokeDictionary.Add(StrokeType.name_TN, new StrokeMeta(15, StrokeType.name_TN,     "ti na ㇝", none, new string[] { "8th stroke of 廻" }));
            strokeDictionary.Add(StrokeType.name_S, new StrokeMeta(16, StrokeType.name_S,       "shu丨", none, new string[] { "4th stroke of 中" }));
            strokeDictionary.Add(StrokeType.name_SG, new StrokeMeta(17, StrokeType.name_SG,     "shu gou ㇚", none, new string[] { "1st stroke of 水" }));
            strokeDictionary.Add(StrokeType.name_ST, new StrokeMeta(18, StrokeType.name_ST,     "shu ti ㇙", none, new string[] { "3rd stroke of 民" }));
            strokeDictionary.Add(StrokeType.name_SP, new StrokeMeta(19, StrokeType.name_SP,     "shu pie", none, new string[] { "1st stroke of 月" }));
            strokeDictionary.Add(StrokeType.name_SW, new StrokeMeta(20, StrokeType.name_SW,     "shu wan ㇄", straightslanted, new string[] { "4th stroke of 四", "3rd stroke of 亡" }));
            strokeDictionary.Add(StrokeType.name_SWZ, new StrokeMeta(21, StrokeType.name_SWZ,   "shu wan zhe ㇘", none, new string[] { "6th stroke of 肅" }));
            strokeDictionary.Add(StrokeType.name_SWG, new StrokeMeta(22, StrokeType.name_SWG,   "shu wan gou ㇟", shortlong, new string[] { "3rd stroke of 己", "7th stroke of 乱" }));
            strokeDictionary.Add(StrokeType.name_SZ, new StrokeMeta(23, StrokeType.name_SZ,     "shu zhe ㇗", straightslanted, new string[] { "2nd stroke of 山", "2nd stroke of 东" }));
            strokeDictionary.Add(StrokeType.name_SZP, new StrokeMeta(24, StrokeType.name_SZP,   "shu zhe pie ㇞", none, new string[] { "not represented in CJK unicode" }));
            strokeDictionary.Add(StrokeType.name_SZZ, new StrokeMeta(25, StrokeType.name_SZZ,   "shu zhe zhe ㇞", shortlong, new string[] { "1st stroke of 卐", "4th stroke of 亞" }));
            strokeDictionary.Add(StrokeType.name_SZWG, new StrokeMeta(26, StrokeType.name_SZWG, "shu zhe wan gou ㇉", shortlong, new string[] { "3rd stroke of 弓", "2nd stroke of 马" }));
            strokeDictionary.Add(StrokeType.name_P, new StrokeMeta(27, StrokeType.name_P,       "pie ㇀", none, new string[] { "1st stroke of 乏" }));
            strokeDictionary.Add(StrokeType.name_PZ, new StrokeMeta(28, StrokeType.name_PZ,     "pie zhe ㇜", shortlong, new string[] { "3rd stroke of 公", "4th stroke of 弘" }));
            strokeDictionary.Add(StrokeType.name_PD, new StrokeMeta(29, StrokeType.name_PD,     "pie dian ㇛", shortlong, new string[] { "1st stroke of 巡", "1st stroke of 女" }));
            strokeDictionary.Add(StrokeType.name_PG, new StrokeMeta(30, StrokeType.name_PG,     "pie gou ㇢", none, new string[] { "1st stroke of 乄" }));
            strokeDictionary.Add(StrokeType.name_D, new StrokeMeta(31, StrokeType.name_D,       "dian ㇔", forwardbackward, new string[] { "3rd stroke of 丸", "8th stroke of 喜" }));
            strokeDictionary.Add(StrokeType.name_N, new StrokeMeta(32, StrokeType.name_N,       "na ㇏", none, new string[] { "3rd stroke of 大" }));
            strokeDictionary.Add(StrokeType.name_WG, new StrokeMeta(33, StrokeType.name_WG,     "wan gou ㇁", none, new string[] { "2nd stroke of 狐" }));
            strokeDictionary.Add(StrokeType.name_XG, new StrokeMeta(34, StrokeType.name_XG,     "xie gou ㇂", shortlong, new string[] { "5th stroke of 我", "2nd stroke of 心" }));
        }

        #region STOKE META API
        /// <summary>
        /// load model from playerprefs. Used by UI
        /// </summary>
        public void Init()
        {
            string strokeName = PlayerPrefs.GetString("StrokeName", StrokeType.name_H);
            string strokeVariant = PlayerPrefs.GetString("StrokeVariant", StrokeType.variant_straight);
            this.ConfigModel(strokeName, strokeVariant);
        }
        #endregion

        #region STROKE DATA API
        public void ConfigModel(string _name, string variant)
        {
            strokeClassification.name = _name;
            strokeClassification.variant = variant;
            strokeClassification.test = StrokeType.test_positive;
            positiveList = GetUnistrokesFromFile(strokeClassification);
            strokeClassification.test = StrokeType.test_negative;
            negativeList = GetUnistrokesFromFile(strokeClassification);
            Debug.Log("Initialize Model | positiveList:" + positiveList.Count + " | negativeList:"+ negativeList.Count);
        }

        public List<Unistroke> GetDataSet(StrokeTypeTest test)
        {
            switch (test)
            {
                case StrokeTypeTest.Negative:
                    if (negativeList.Count == 0)
                    {
                        strokeClassification.test = StrokeType.test_negative;
                        negativeList = GetUnistrokesFromFile(strokeClassification);
                    }
                    return new List<Unistroke>(negativeList);
                case StrokeTypeTest.Positive:
                    if (positiveList.Count == 0)
                    {
                        strokeClassification.test = StrokeType.test_positive;
                        positiveList = GetUnistrokesFromFile(strokeClassification);
                    }
                     return new List<Unistroke>(positiveList);

                default:
                    if (positiveList.Count == 0)
                    {
                        strokeClassification.test = StrokeType.test_positive;
                        positiveList = GetUnistrokesFromFile(strokeClassification);
                    }
                    if (negativeList.Count == 0)
                    {
                        strokeClassification.test = StrokeType.test_negative;
                        negativeList = GetUnistrokesFromFile(strokeClassification);
                    }
                    List<Unistroke> result = new List<Unistroke>(positiveList);
                    result.AddRange(negativeList);
                    return result;
            }
            
        }

        public void SaveDataSet(string test)
        {
            strokeClassification.test = test;
            if(test == StrokeType.test_positive)
                SaveUnistrokesToFile(positiveList, strokeClassification);
            else if (test == StrokeType.test_negative)
                SaveUnistrokesToFile(negativeList, strokeClassification);
        }
        #endregion

        #region PRIVATE METHODS
        private List<Unistroke> GetUnistrokesFromFile(StrokeClassification sc)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + ClassToFileName(sc);
            Debug.Log("load file: " + path);
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            List<Unistroke> unistrokes;
            if (new FileInfo(path).Length > 0)
            {
                List<_Unistroke> data = bf.Deserialize(file) as List<_Unistroke>;
                

                unistrokes = new List<Unistroke>(data.Count);
                foreach (_Unistroke u in data)
                    unistrokes.Add(new Unistroke(u));
            }
            else
            {
                unistrokes = new List<Unistroke>();
            }
            file.Close();
            return unistrokes;
        }

        private void SaveUnistrokesToFile(List<Unistroke> unistrokes, StrokeClassification sc)
        {
            List<_Unistroke> _unistrokes = new List<_Unistroke>();
            foreach(Unistroke u in unistrokes)
            {
                _unistrokes.Add(u._unistroke);
            }
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + ClassToFileName(sc);
            Debug.Log("save file: " + path);
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            bf.Serialize(file, _unistrokes);
            file.Close();
        }

        private string ClassToFileName(StrokeClassification sc)
        {
            return "/" + sc.name + "_" + sc.variant + "_" + sc.test + ".dat";
        }
        #endregion
    }
}