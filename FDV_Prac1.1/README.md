# Práctica 1 FDV
## Entorno Unity 3D

### Carga de la escena starter assets Third Person
El paquete de assets Third Person, es un paquete que se encuentra en la Asset Store de Unity. Para utilizarlo en nuestro proyecto debemos previamente añadirlo a nuestros assets, de esta manera aparecerá en el "package manager" (windows > package manager). Tal y como se muestra en el gif descargaremos, importaremos el paquete e instalaremos o actualizaremos las dependencias del mismo como se aprecia en el siguiente gif.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.gif "Importacion")

Una vez termine el proceso podremos importar los assets.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.2.gif "Importacion")

Para abrir la escena demo que incluye el paquete de assets debemos dirigirnos a **StarterAssets > ThirdPersonController > Scenes**

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.3.gif "Escena")

Podremos observar que todos los GameObjects de la escena son de color rosa, esto se debe a que el asset que hemos importado está utilizando el pipeline de renderizado URP y en nuestro caso estamos utilizando el "Built-in pipeline" al usar la plantilla 3D de Unity. Para poder ver los materiales correctamente tendremos que ir a **Edit > Project Settings > Graphics > URP Global Settings** y pinchar en fix y en Graphics, en **Scriptable Render pipeline Settings** usaremos el StarterAssetsURPAsset en lugar de None.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.4.gif "Rendering")

---
### Eliminar entorno
Seleccionaremos todos los GameObject que conforman la escena y los eliminaremos, dejando el suelo para que el jugador no caiga infinitamente.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/2.gif "Removing environment")

---
### Incluir GameObjects
Incluiremos ahora una **rampa y un cubo** creando una nueva composición.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/3.gif "Adding GameObjects")

---
### Incluir GameObjects de la Asset Store
En este caso, se ha optado por incluir un **GameObject de una moneda** en lo alto del cubo.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/4.gif "Importing Coin")

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/4.1.gif "Adding Coin")

---
### Convertir uno de los GameObjects incluido en el personaje
Se ha cambiado la geometría del personaje para que se **muestre un cubo**, aprovechando así la lógica del movimiento ya programada.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/5.gif "Cube Moving")

---
### Agregar fuente de iluminación
Se ha añadido una fuente de iluminación y se han modificado los parámetros de **Emission > Color** a un color **verde** y se ha aumentado **Emission > Intensity** para poder observar con mayor facilidad el color de la luz.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/6.gif "Luz")

---
### Generar script que muestre por consola un mensaje
Por último, se ha añadido un script para mostrar en el Start del mismo "Hello World" por consola.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
```

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/7.gif "Script")

## GIT LFS
Se ha utilizado git lfs para subir el .zip del proyecto, para ello se ha utilizado el siguiente comando
```
git lfs track "*.zip"
```
Una vez ejecutado este comando se nos crea un fichero .gitattributes
![image](https://github.com/alu0101030531/FDV_Practicas/assets/43813200/9680af4f-9eea-4837-a20b-fbf90be7e77a)




