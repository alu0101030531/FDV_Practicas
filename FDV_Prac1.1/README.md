# Práctica 1 FDV
## Entorno Unity 3D

### Carga de la escena starter assets Third Person
El paquete de assets Third Person, es un paquete que se encuentra en la Asset Store de Unity. Para utilizarlo en nuestro proyecto debemos previamente añadirlo a nuestros assets, de esta manera aparecerá en el "package manager" (windows > package manager). Tal y como se muestra en el gif, descargaremos, importaremos el paquete e instalaremos o actualizaremos las dependencias del mismo como se aprecia en el siguiente gif

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.gif "Importacion")

Una vez termine el proceso podremos importar los assets

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.2.gif "Importacion")

Para abrir la escena demo que incluye el paquete de assets debemos dirigirnos a **StarterAssets > ThirdPersonController > Scenes**

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.3.gif "Escena")

Podremos observar que todos los GameObjects de la escena son de color rosa, esto se debe a que el asset que hemos importado está utilizando el pipeline de renderizado URP y en nuestro caso estamos utilizando el "built-in pipeline" al usar la plantilla 3D de Unity. Para poder ver los materiales correctamente tendremos que ir a **Edit > Project Settings > Graphics > URP Global Settings** y pinchar en fix y en Graphics, en **Scriptable Render pipeline Settings** usaremos el StarterAssetsURPAsset en lugar de None.

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/1.4.gif "Rendering")

---
### Eliminar entorno
Seleccionaremos todos los GameObject que conforman la escena y los eliminaremos, dejando el suelo para que el jugador no caiga infinitamente

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/2.gif "Removing environment")

---
### Incluir GameObjects
Incluiremos ahora una rampa y un cubo creando una nueva composición

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/3.gif "Adding GameObjects")

---
### Incluir GameObjects de la Asset Store
En este caso se ha optado por incluir un GameObject de una moneda en lo alto del cubo

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/4.gif "Importing Coin")

![alt text](https://github.com/alu0101030531/FDV_Practicas/blob/main/FDV_Prac1.1/Readme_Images/4.1.gif "Adding Coin")
