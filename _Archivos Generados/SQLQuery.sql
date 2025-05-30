SELECT TOP 1
    sub.User_id,
    CAST(SUM(sub.SegundosSesion) / 86400 AS VARCHAR) + ' días, ' +
    CAST((SUM(sub.SegundosSesion) % 86400) / 3600 AS VARCHAR) + ' horas, ' +
    CAST((SUM(sub.SegundosSesion) % 3600) / 60 AS VARCHAR) + ' minutos, ' +
    CAST(SUM(sub.SegundosSesion) % 60 AS VARCHAR) + ' segundos' AS [Tiempo total]
FROM (
    SELECT
        logs.User_id,
        logs.TipoMov,
        DATEDIFF(SECOND, logs.fecha, LEAD(logs.fecha) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha)) AS SegundosSesion,
        LEAD(logs.TipoMov) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha) AS NextTipoMov
    FROM ccloglogin AS logs
) AS sub
WHERE sub.TipoMov = 1 AND sub.NextTipoMov = 0
GROUP BY sub.User_id
ORDER BY SUM(sub.SegundosSesion) DESC;


SELECT TOP 1
    sub.User_id,
    CAST(SUM(sub.SegundosSesion) / 86400 AS VARCHAR) + ' días, ' +
    CAST((SUM(sub.SegundosSesion) % 86400) / 3600 AS VARCHAR) + ' horas, ' +
    CAST((SUM(sub.SegundosSesion) % 3600) / 60 AS VARCHAR) + ' minutos, ' +
    CAST(SUM(sub.SegundosSesion) % 60 AS VARCHAR) + ' segundos' AS [Tiempo total]
FROM (
    SELECT
        logs.User_id,
        logs.TipoMov,
        DATEDIFF(SECOND, logs.fecha, LEAD(logs.fecha) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha)) AS SegundosSesion,
        LEAD(logs.TipoMov) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha) AS NextTipoMov
    FROM ccloglogin AS logs
) AS sub
WHERE sub.TipoMov = 1 AND sub.NextTipoMov = 0
GROUP BY sub.User_id
HAVING SUM(sub.SegundosSesion) > 0
ORDER BY SUM(sub.SegundosSesion) ASC;


SELECT
    sub.User_id,
    FORMAT(sub.FechaLogin, 'yyyy-MM') AS Mes,
    CAST(AVG(sub.SegundosSesion) / 86400 AS VARCHAR) + ' días, ' +
    CAST((AVG(sub.SegundosSesion) % 86400) / 3600 AS VARCHAR) + ' horas, ' +
    CAST((AVG(sub.SegundosSesion) % 3600) / 60 AS VARCHAR) + ' minutos, ' +
    CAST(AVG(sub.SegundosSesion) % 60 AS VARCHAR) + ' segundos' AS [Promedio de sesión]
FROM (
    SELECT
        logs.User_id,
        logs.fecha AS FechaLogin,
        DATEDIFF(SECOND, logs.fecha, LEAD(logs.fecha) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha)) AS SegundosSesion,
        LEAD(logs.TipoMov) OVER (PARTITION BY logs.User_id ORDER BY logs.fecha) AS NextTipoMov
    FROM ccloglogin AS logs
) AS sub
WHERE sub.NextTipoMov = 0
GROUP BY sub.User_id, FORMAT(sub.FechaLogin, 'yyyy-MM')
ORDER BY sub.User_id, Mes;
