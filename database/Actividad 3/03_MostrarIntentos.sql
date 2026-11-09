USE HPA_Parcial3_BCrypt;

SELECT
    IdIntento,
    IdUsuario,
    UsuarioIngresado,
    CASE WHEN Exitoso = 1 THEN N'Exitoso' ELSE N'Fallido' END AS Resultado,
    Fecha
FROM dbo.IntentosAcceso
ORDER BY IdIntento;