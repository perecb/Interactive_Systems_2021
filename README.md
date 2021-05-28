# Interactive_Systems_2021

The Path ahora mismo consiste en un avatar el cual se movera hacia la derecha de manera constante. Su objetivo es llegar desde la plataforma amarilla a la plataforma verde.
Con los dedos indices de cada mano, el usuario controlara unas esferas que se pueden ver en pantalla. Entre ellas se va ha generar un puente por el que el avatar podra pasar, aunque este puente siempre se puede activar y desactivar haciendo que las dos esferas entren en contacto.
Ahora mismo solo existe un nivel jugable pero si se puede ver la mecanica de pasar de un nivel al otro cuando llegas a la plataforma verde.

Es importante tener instalado el mediapipe unity el cual encontraras informacion en esta web: https://google.github.io/mediapipe/getting_started/python.html

Tambien necesitas tener python con la version 3.6, 3.7 o 3.8: https://www.python.org/ftp/python/3.8.8/python-3.8.8-amd64.exe

Para instalar el mediapipe sigue estos pasos desde la terminal (cmd):
    Desde la línea de comandos, creamos undirectorio
      > mkdir mediapipeunity
    Ejecutamos un comando para crear un ambiente virtual de python
      > python -m venv mp_env
    Activamos el ambiente virtual - se verá (mp_env)
      > .\mp_env\Scripts\activate
    Instalamos mediapipe
      >pip install mediapipe
      
Ahora se debe introducir el archivo de nuestro repositorio hands.py en el directorio que has creado y ejecutarlo con el siguiente comando.
      > python hands.py
     
Debes dejar la ventana abierta y ejecutar el programa en unity.
