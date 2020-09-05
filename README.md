# PCharacter
An easy way to create 2D characters for your Unity Games without using Animator and animations clip.

## Setup

Depois de ter baixado o PCharacter, apenas importe a pasta para o seu projeto Unity.<br>
Em questão de performance, PCharacter utilizar o SpriteAtlas da Unity na hora de renderizar os frames do personagem. <br> 
Dito isso, para todo personagem que for criar no seu jogo utilizando o PCharacter, é necessário que tenha criado um SpriteAtlas para ele também.

## Create your Character

O gif abaixo demonstra todo o processo da criação de um PCharacter e as suas animações.

<p align="center"> 
<img src="https://media.giphy.com/media/efPKFgVjHuEO7BoUZV/giphy.gif" style="max-height: 300px;">
</p>

### Explanation 

- Existem dois tipos de PCharacter, o CHARACTER e o SOLID. A única diferença entre eles é o Rigidbody, se o PCharacter creado for do tipo SOLID, ele não irá ter um Rigidbody aplicado
em seus componentes.

- Os PCharacters utilizam uma GlobalConfiguration para ajudar a definir os valores globais que todos os personagem irão ter - Velocidade da animação, gravidade,
freeze rotation, etc... -. Dentro da pasta do PCharacter já tem um criado como padrão. 

- **Update atlas before adding new animations:** Antes de adicionar novas animações para os personagens o PCharacter precisa de um SpriteAtlas de onde ele vai definir os sprites
de cada frame.

## Adding your character to the game

- Todos os Character criados no seu projeto servirão de base para os Character Instances presentens no seu jogo, são eles que serão adicionados na sua cena.

- Todos os comandos relacionados ao PCharacter são encontrados dentro da namespace PCharacter <br>
```cs
using namespace PCharacter;
```

##### Creating PCharacter instance from base:

Para instanciar um novo PCharacterInstance atráves de uma base criada, apenas utilize a função **CreateInstanceFromBase**.


```cs
public Character Base;

void Start()
{
  PCharacterInstance instance = PCharacterInstanceUtil.CreateInstanceFromBase(Base);  
  instance.transform.position = Vector2.zero;
}
```

#### Or set it up on Scene directly

Se quiser também, pode criar um objecto vazio na cena e adicionar o componenent **PCharacterInstance**, definir a base dele e um nome. Assim, autimaticamente o sprite do personagem
será definido junto com seu Rigidbody e seu BoxCollider2D

## Playing animations

It will need two main functions to play and update animations with PCharacter:
- **SetInstanceAnimation:** Defines what animation will be played to the character
- **UpdateInstanceAnimation:** Updates the current animation. Put it inside the Update function

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

Sinta-se livre para implementar os movimentos do PCharacterInstance, mas o PCharacter Lybrari já tem uma implementação bem básica para os movimentos do personagem,
como andar e pular.

Para isso, irão ser necessárias duas funções:<br>
- **AddInstanceControllerAction:** Adiciona um tipo de ação para o PCharacterInstance realizar - As ações podem ser entre: **TRANSLATE_LEFT, TRANSLATE_RIGHT, JUMP**.
- **UpdateInstanceController:** Atualiza a lista de ações que o PCharacterInstance tem.

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

- **IsCharacterGrounded:** Retorna se o posonagem está encostado no chão ou não.
```cs
public static bool IsCharacterGrounded(Vector2 detectorOrigin, float radius, LayerMask groundLayer) { ... }
```

- **BreakControllerWhenFacingWall:** Evita que o personagem fique andando constantemente contra uma parede
```cs
public static void BreakControllerWhenFacingWall(IPCharacterInstance instance, LayerMask targetLayer, float detectorDistance = 1f) { ... }
```

- **RecalculateCollider:** Set collider2D values to boudaires of the current rendered sprite of the PCharacterInstance
```cs
public static void RecalculateCollider(PCharacterInstance instance) { ... }
```

# License 

PCharacter is a free software; you can redistribute it and/or modify it under the terms of the MIT license. See LICENSE for details.
