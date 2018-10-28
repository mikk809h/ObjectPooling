using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    /// <summary>
    ///     Name of the object pool and the objects that are instantiated.
    /// </summary>
    public string name;

    /// <summary>
    ///     How many objects to instantiate when starting.
    /// </summary>
    public int size;

    /// <summary>
    ///     The object to pool.
    /// </summary>
    public GameObject prefab;

    /// <summary>
    ///     The transform of the gameobject which will be the parent for every object in the pool.
    /// </summary>
    public Transform parent;

    /// <summary>
    ///     Private list of pooled objects.
    /// </summary>
    private List<GameObject> pool = new List<GameObject>();

    /// <summary>
    ///     Constructor of the Object Pool.
    /// </summary>
    /// <param name="name">Name of the object pool.</param>
    /// <param name="size">Size of the pool.</param>
    /// <param name="prefab">The object to pool.</param>
    /// <param name="parent">The transform of the gameobject which will be the parent for every object in the pool.</param>
    public ObjectPool(string name, int size, GameObject prefab, Transform parent)
    {
        if (System.String.IsNullOrEmpty(name))
        {
            Debug.Log("[ObjectPool] Name invalid");
            return;
        }

        // Set all attributes
        this.name = name;
        this.size = size;
        this.prefab = prefab;
        this.parent = parent;

        // Set-up the objects into the game and add to the pool.
        for (var i = 0; i < size; i++)
        {
            // Instantiates the pooled object under the parent
            GameObject obj = Object.Instantiate(prefab, parent);

            // Sets the object's position to <0, 0, 0>
            obj.transform.position = Vector3.zero;

            // Sets the name of the object to the defined name of the ObjectPool
            obj.name = name;

            // Disables the object and adds it to the pool
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    /// <summary>
    ///     Gets an object from the pool, activates it and sets the position and rotation.
    /// </summary>
    /// <param name="position">Position to place returned object.</param>
    /// <param name="rotation">Rotation to set the object to.</param>
    /// <returns>An object from the pool.</returns>
    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        // If no remaining objects are ready to be taken from the pool,
        // Create a new object and return it
        if (pool.Count == 0)
        {
            // Instantiate the object and set the name
            GameObject iObject = Object.Instantiate(prefab, parent);
            iObject.name = name;

            // Set the object to the desired position and rotation
            iObject.transform.position = position;
            iObject.transform.rotation = rotation;

            return iObject;
        }

        // If there are remaining objects ready to be taken from the pool:
        // Grab an object from the pool
        GameObject newObject = pool[pool.Count - 1];

        // Set the object to the desired position and rotation
        newObject.transform.position = position;
        newObject.transform.rotation = rotation;

        // Activate the object
        newObject.SetActive(true);
        
        // Remove it from the pool
        pool.Remove(newObject);

        return newObject;
    }

    /// <summary>
    ///     Disables the object and puts it back into the pool.
    /// </summary>
    /// <param name="gameObject">A object from the pool.</param>
    public void ReturnObjectToPool(GameObject gameObject)
    {
        // Resets the position and disables the object
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false);

        // Adds the object to the pool
        pool.Add(gameObject);
    }
}
