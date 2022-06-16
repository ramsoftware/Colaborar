# Colaborar http://darwin.50webs.com/Espanol/Colaboracion.htm
Colaboración entre individuos

Dos o más individuos distintos colaboran entre sí para afrontar un ambiente. El primer paso para entender esta colaboración fue volver al principio de esta investigación: dada una serie de puntos (X,Y) donde el valor de X no se repite, hallar la mejor curva, es decir, la función de Y=f(X) que mejor se aproxime a esos puntos. ¿La razón? Al tener esa curva se podría hacer extrapolación, es decir, predecir los valores de Y al llevar los valores de X más allá de los límites de los datos dados. Ese fue el tema central de los primeros dos capítulos de esta investigación y se examinó a fondo en investigaciones realizadas como proyectos de investigación en la Universidad Libre – Cali y que dio origen a un par de libros y software.

Viene un cambio radical a todo eso, en primer lugar, se le quita toda la importancia a la extrapolación, solo dio problemas porque los resultados no fueron precisos, debido a que los datos en sí pueden cambiar completamente su comportamiento más allá del conjunto dado o la mejor curva presenta comportamientos extremos al extrapolar. Por ejemplo, los polinomios tienden a valores de Y infinito o menos infinito al llevar a X a valores negativos o positivos grandes. En polinomios, por lo general, el comportamiento no es extremo si el valor de X se acerca a cero sea por el lado positivo o negativo.

Otro gran cambio, es quitar importancia de hallar una y solo una curva que se ajustara a todos los datos dados. Se plantea entonces hacer uso de dos o más curvas, es decir, dos o más funciones, donde cada una se encargue de un subconjunto de los datos dados. Esto tiene más sentido si se deja de lado la extrapolación.

Y este es el inicio de la investigación de la colaboración entre individuos:

1. Hacer software donde se divida el conjunto de datos y cada subconjunto tendrá su propia curva de ajuste.
2. Lo importante es la interpolación, no la extrapolación.

El lector podría juzgar rápidamente que el problema que presenta este enfoque es que, si se tiene un conjunto de N datos, se pueden generar N-1 rectas con ajuste perfecto, y con esas rectas se puede hacer interpolación muy aceptable. Y tendría razón el lector, salvo que eso funciona con una sola variable independiente.
La realidad es muy compleja, gran cantidad de variables afectan el comportamiento de los ambientes, luego ya no tiene sentido hablar de Y=F(X) en vida artificial, sino del tipo K=F(A,B, C, …. T) o más variables.
