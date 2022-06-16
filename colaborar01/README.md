http://darwin.50webs.com/Espanol/Colaborar01.htm

Algoritmos genéticos. Programa 1.
Parte de un concepto simple: Se tiene un conjunto de datos tipo X,Y. El objetivo es hallar la curva Y=F(X)o curvas (Y=F(X), G=F(X), y así sucesivamente) que mejor se ajusten a ese conjunto de datos.

Estos son los pasos:

1. Generar una sola curva Y = F(X) que mejor ajuste a todo el conjunto de datos.

2. Generar una curva Y = F(X) que ajuste a la primera mitad del conjunto datos y luego generar una segunda curva Y = G(X) que ajuste a la segunda mitad.

3. Generar una curva Y = F(X) que ajuste al primer tercio del conjunto datos, luego generar una segunda curva Y = G(X) que ajuste al segundo tercio y una última curva Y=H(X) que ajuste al tercio final.

4. Y así sucesivamente

Se va a utilizar una curva de este estilo:

Y = (Seno(a*(1-X)^4 + b*(1-X)^3 + c*(1-X)^2 + d*(1-X) + e + f*X + g*X^2 + h*X^3 + i*X^4) + 1)/2

Los valores de Y están comprendidos entre 0 y 1, eso significa que el conjunto de datos debe estar normalizado entre 0 y 1.

En el algoritmo genético, como se trabaja con una población, cada individuo de la población es una ecuación de ese estilo, pero con distintos valores a, b, c, d, e, f, g, h, i. El objetivo es hallar el individuo que se ajuste mejor a los datos (saber que valores o coeficientes a,b,c,d,e,f,g,h,i permiten el mejor ajuste).

¿Por qué esa curva de ese estilo? Porque en la naturaleza no hay valores de crecimiento infinito Y, se llega a un tope (por leyes físicas) y comenzaría a disminuir, y podría aumentar de nuevo para luego volver a disminuir, es decir, un comportamiento cíclico, que se representa con una función sinusoidal. También Y podría a tender a cero y esa función puede mostrar ese comportamiento.

¿Podría ser otro tipo de ecuación? Por supuesto, podría usarse coseno o cualquier otra función (recomendado tener en cuenta el comportamiento cíclico) y tener más coeficientes. Cabe recordar que una función muy compleja o con una gran cantidad de coeficientes puede hacer más lento evaluarla, pero muy pocos coeficientes generan muy poca variedad.

El algoritmo genético trabaja así:

Paso 1: Dada una serie de valores (X, Y) donde X es la variable independiente y Y la variable dependiente se usará un algoritmo evolutivo para determinar cuál expresión matemática de tipo Y=F(X) logra ajustarse a esos datos.

Paso 2: Los individuos son del tipo: Y = (Math.Sin(a*(1-x)^4 + b*(1-x)^3 + c*(1-x)^2 + d*(1-x) + e + f*x + g*x^2 + h*x^3 + i*x^4) + 1)/2

Paso 3: La población se compone de N individuos generados al azar

Paso 4: El proceso evolutivo es:
a. Seleccionar dos individuos al azar
b. Evaluar cada individuo escogido que tanto ajusta con la salida esperada
c. El individuo con mejor ajuste sobreescribe al de menor ajuste
d. El nuevo individuo (copia del mejor) se modifica al azar
Paso 5: El proceso evolutivo se repite N veces

Paso 6: Se escanea toda la población por el mejor individuo (mejor ajuste)

Paso 7: El mejor individuo muestra sus valores de salida dado los valores de entrada
