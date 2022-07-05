# LambdaMutant Project
Corresponde a la segundo desafio del examen, el cual es una API llamada Mutant, con el recurso mutant, desplegada en AWS, esta API tiene como backend una Lambda como runtime .NET que se encarga detecta si el ADN ingresado corresponde a un mutante o humano.

## Here are some steps to call it:
1. Desde un cliente API REST, consumir desde un metodo POST, la siguiente url: https://90bp2odm09.execute-api.us-east-1.amazonaws.com/prod/
2. Incluir en el header el atributo Content-Type: application/json
3. Incluir en el Body en formato json el dna a validar. por ejemplo {"dna":["TCGAAA","CCCCGT","AAAATC","CCTTGG","GGGTAA","TCGAAA"]}