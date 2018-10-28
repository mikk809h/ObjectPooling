# ObjectPooling
Object Pooling Class for Unity

#### Usage:
```
ObjectPool(string name, int 10, GameObject object, Transform parent)

// Construct the pool
ObjectPool ballPool = new ObjectPool("Ball", 10, GameObject.Find("Ball"), Transform.Find("Balls"))

// Get an object from the pool
GameObject ball = ballPool.GetObject(transform.position, transform.rotation);

// Return the object
ballPool.ReturnObject(ball);
```

#### Description:
Improves performance by saving and instantiating objects at the launch of the game.
