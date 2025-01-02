using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.ScribtableObject
{
    [CreateAssetMenu(fileName = "NewStringTypeList", menuName = "ScriptableObjects/StringTypeList", order = 1)]
    public class ResourceTypes : ScriptableObject
    {
        public List<StringTypeSO> stringTypes = new List<StringTypeSO>();
    }

    [System.Serializable]
    public struct StringTypeSO
    {
        public string typeName; // The name of the type, e.g., "Stock", "Buy"
        [TextArea]
        public string description; // Optional: Description of the type
        public Sprite icon; // Optional: An icon for the type
    }
}

