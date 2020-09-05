# PCharacter
An easy way to create 2D characters for your Unity Games without using Animator and animations clip.

## Setup

Import the PCharacter folder into your Unity's project after downloaded the Library.<br>
PCharacter uses SpriteAtlas from unity to render every created character.

## Create your Character

The gif below demonstrates the whole process of creating and setting up the PCharacter animation.

<p align="center"> 
<img src="https://media.giphy.com/media/efPKFgVjHuEO7BoUZV/giphy.gif" style="max-height: 300px;">
</p>

### Explanation 

- There are two types of PCharacter, the CHARACTER, and SOLID ones. The only difference between them is that SOLID does not use Rigidbody2D. So, if the created PChracter is from SOLID type, it will not have physics to be applied on. 

- The PCharacters uses a GlobalConfiguration to defines all global values for every instantiated character of that PCharacter - Such as animation speed, gravity, freeze rotation, etc -. Inside PCharacter's folder, there is already a GlobalConfiguration created by default.

- **Update atlas before adding new animations:** Before adding any animations to the PCharacters, make sure that you've already created a SpriteAtlas and set it up in Character Atlas field. After that, click on _Update atlas_ button to update it sprites.

## Adding your characters to the game

- Every PCharacter will serve as a base to instantiate the PCharacterInstances. 

- All commands related to PCharacter will be in PCharacter namespace<br>
```cs
using namespace PCharacter;
```

##### Creating PCharacter instance from base:

To instantiate a new PCharacterInstance from base, just uses the **CreateInstanceFromBase** command in PCharacterInstanceUtil class.

```cs
public Character Base;

void Start()
{
  PCharacterInstance instance = PCharacterInstanceUtil.CreateInstanceFromBase(Base);  
  instance.transform.position = Vector2.zero;
}
```

#### Or set it up directly on Scene

Also, if you want to create a PCharacterInstance directly in Hierarchy, just create an Empty object and add the **PCharacterInstance** component to it. After defining its base and set it up its Name, unity will automatically define the rest of the values for you.

## Playing animations

It will need two main functions to play and update animations with PCharacter:
-  **SetInstanceAnimation:** Defines what animation will be played to the character
-  **UpdateInstanceAnimation:** Updates the current animation. Put it inside the Update function

Example:
```cs

public Character Base;
public PCharacterInstance Instance;

void Start()
{
  Instance = PCharacterInstanceUtil.CreateInstanceFromBase(Base);  
  Instance.transform.position = Vector2.zero;
  
  PCharacterInstanceUtil.SetInstanceAnimation(Instance, "Idle");
}

void Update()
{
  PCharacterInstanceUtil.UpdateInstanceAnimation(Instance);
}
```

## Controlling the PCharacterInstance

Feel free to implement the movements as you want to the PCharacterInstance, but the PCharacter lib already has a basic implementation of basic movements for you to use - Walk and Jump -.

It will need two main functions to controll your PCharacterInstance:<br>
- **AddInstanceControllerAction:** Adds actions type for the PCharacterInstance to use - Actions can be: **TRANSLATE_LEFT, TRANSLATE_RIGHT, JUMP**.
- **UpdateInstanceController:** Updates every added action in PCharacterInstance.

Example:
```cs

public Character Base;
public PCharacterInstance Instance;

float moveSpeed = 5;
float jumpForce = 10;

void Start()
{
  Instance = PCharacterInstanceUtil.CreateInstanceFromBase(Base);  
  Instance.transform.position = Vector2.zero;
  
  PCharacterInstanceUtil.SetInstanceAnimation(Instance, "Idle");
}

void Update()
{
  PCharacterInstanceUtil.UpdateInstanceAnimation(Instance);
  
  if(Input.GetKey(KeyCode.D))
    PCharacterInstanceUtil.AddInstanceControllerAction(Instance, PControllerActionType.TRANSLATE_RIGHT);
  else if(Input.GetKey(KeyCode.A))
    PCharacterInstanceUtil.AddInstanceControllerAction(Instance, PControllerActionType.TRANSLATE_LEFT);
    
   if(Input.GetKeyDown(KeyCode.Space))
    PCharacterInstanceUtil.AddInstanceControllerAction(Instance, PControllerActionType.JUMP);
    
   PCharacterInstanceUtil.UpdateInstanceController(Instance, moveSpeed, jumpForce, faceToWalkDirection: true);
}
```

## Some additional functions

Here some more functions that can be usefull for your programming:

- **IsCharacterGrounded:** Returns if the character is raycast detecting a certain layer.
```cs
public static bool IsCharacterGrounded(Vector2 detectorOrigin, float radius, LayerMask groundLayer) { ... }
```

- **BreakControllerWhenFacingWall:** Breaks all actions to avoid walk against every facing wall
```cs
public static void BreakControllerWhenFacingWall(IPCharacterInstance instance, LayerMask targetLayer, float detectorDistance = 1f) { ... }
```

- **RecalculateCollider:** Set collider2D values to boudaires of the current rendered sprite of the PCharacterInstance
```cs
public static void RecalculateCollider(PCharacterInstance instance) { ... }
```

# License 

PCharacter is a free software; you can redistribute it and/or modify it under the terms of the MIT license. See LICENSE for details.
