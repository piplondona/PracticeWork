# LambdaMutantStats Project
Corresponde a la tecer desafio del examen, el cual es una API llamada Mercado Libre, con el recurso stats, desplegada en AWS, esta API tiene como backend una Lambda como runtime .NET que se encarga calcular el ratio de los ADNs detectados como mutante o humano, consultando la base de datos donde se encuentran los DNAs

## Here are some steps to call it:
1. Desde un cliente API REST, consumir desde un metodo POST, la siguiente url: https://sc45mqicp8.execute-api.us-east-1.amazonaws.com/cert/stats
2. No se debe incluir nada en el header.
3. No se debe incluir nada en el Body