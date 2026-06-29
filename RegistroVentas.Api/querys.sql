SELECT * FROM RegistrosOperaciones;

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones;

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
WHERE Pk = 1;

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
WHERE Referencia = '12345678';

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
WHERE Estatus = 'Aprobado';

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
WHERE Estatus = 'Cancelado';

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
ORDER BY Pk
LIMIT 10 OFFSET 0;

SELECT 
    Pk,
    Operacion,
    Importe,
    Cliente,
    Referencia,
    Estatus
FROM RegistrosOperaciones
ORDER BY Pk
LIMIT 10 OFFSET 10;


SELECT COUNT(*) AS Total
FROM RegistrosOperaciones;