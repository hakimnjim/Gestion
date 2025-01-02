using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Global.ScribtableObject
{
    [CreateAssetMenu(fileName ="NewCollectionList", menuName = "ScriptableObjects/CollectionsList", order = 2)]
    public class Collections : ScriptableObject
    {
        public List<CollectionEntity> defualtEntitys;
        public List<Collection> allCollections;
    }

    [System.Serializable]
    public class CollectionEntity
    {
        public CollectionEntityType collectionEntityType;
        public string input;
        //public bool hasThisEntity;
        public string nameOfEntity;
    }

    [System.Serializable]
    public class Collection
    {
        public string collectionName;
        public List<CollectionEntity> collectionEntities = new List<CollectionEntity>();
        public List<Collection> collections = new List<Collection>();
        public bool hasStringTypeSO;
        public DateTime dateTime;

        public Collection DeepCopy()
        {
            Collection newCollection = new Collection
            {
                collectionName = this.collectionName,
                hasStringTypeSO = this.hasStringTypeSO,
                dateTime = this.dateTime,
                collectionEntities = new List<CollectionEntity>(),
                collections = new List<Collection>()
            };

            // Copy entities
            foreach (var entity in collectionEntities)
            {
                newCollection.collectionEntities.Add(new CollectionEntity
                {
                    collectionEntityType = entity.collectionEntityType,
                    input = entity.input,
                    nameOfEntity = entity.nameOfEntity
                });
            }

            // Copy nested collections
            foreach (var subCollection in collections)
            {
                newCollection.collections.Add(subCollection.DeepCopy());
            }

            return newCollection;
        }
    }

    public enum CollectionEntityType { isString, isFloat, isInteger, isCollection, isType}

}
