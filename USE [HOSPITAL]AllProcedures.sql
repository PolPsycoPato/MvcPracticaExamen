USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_PLANTILLA_UPSERT]    Script Date: 05/02/2026 19:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 1. SP UPSERT (Insertar o Actualizar)
ALTER   PROCEDURE [dbo].[SP_PLANTILLA_UPSERT]
    @HOSPITAL_COD INT,
    @SALA_COD INT,
    @EMPLEADO_NO INT,
    @APELLIDO NVARCHAR(50),
    @FUNCION NVARCHAR(50),
    @T NVARCHAR(1) = NULL, -- Permitimos NULL por defecto
    @SALARIO INT
AS
BEGIN
    -- Verificamos si existe el empleado
    IF EXISTS (SELECT 1 FROM PLANTILLA WHERE EMPLEADO_NO = @EMPLEADO_NO)
    BEGIN
        -- ACTUALIZAR
        UPDATE PLANTILLA SET 
            HOSPITAL_COD = @HOSPITAL_COD,
            SALA_COD = @SALA_COD,
            APELLIDO = @APELLIDO,
            FUNCION = @FUNCION,
            T = @T,
            SALARIO = @SALARIO
        WHERE EMPLEADO_NO = @EMPLEADO_NO
    END
    ELSE
    BEGIN
        -- INSERTAR
        INSERT INTO PLANTILLA (HOSPITAL_COD, SALA_COD, EMPLEADO_NO, APELLIDO, FUNCION, T, SALARIO)
        VALUES (@HOSPITAL_COD, @SALA_COD, @EMPLEADO_NO, @APELLIDO, @FUNCION, @T, @SALARIO)
    END
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerNombresHospitales]    Script Date: 05/02/2026 19:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_ObtenerNombresHospitales]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        H.HOSPITAL_COD, 
        H.NOMBRE
    FROM 
        [HOSPITAL].[dbo].[HOSPITAL] H
    INNER JOIN 
        [HOSPITAL].[dbo].[PLANTILLA] P 
        ON H.HOSPITAL_COD = P.HOSPITAL_COD
    ORDER BY 
        H.HOSPITAL_COD;
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerDoctoresPorHospital]    Script Date: 05/02/2026 19:26:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[sp_ObtenerDoctoresPorHospital]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
   
        [APELLIDO] as 'Apellido Doctor',
        [FUNCION] as 'Especialidad/Funcion',
        [SALARIO] as 'Salario'
    
    FROM 
        [HOSPITAL].[dbo].[PLANTILLA]
    WHERE 
        [FUNCION] IN ('DOCTOR') 
    ORDER BY 
        [HOSPITAL_COD] ASC, 
        [APELLIDO] ASC;
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_PLANTILLA_FILTRO]    Script Date: 05/02/2026 19:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 4. SP FILTRO (Buscador)
ALTER   PROCEDURE [dbo].[SP_GET_PLANTILLA_FILTRO]
    @FUNCION NVARCHAR(50)
AS
BEGIN
    SELECT * FROM PLANTILLA WHERE FUNCION = @FUNCION
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_FUNCIONES]    Script Date: 05/02/2026 19:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 3. SP LECTURA DE FUNCIONES (Para el desplegable)
ALTER   PROCEDURE [dbo].[SP_GET_FUNCIONES]
AS
BEGIN
    SELECT DISTINCT FUNCION FROM PLANTILLA WHERE FUNCION IS NOT NULL
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_EMPLEADO_DETALLE]    Script Date: 05/02/2026 19:26:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5. SP DETALLE (Para editar un empleado específico)
ALTER   PROCEDURE [dbo].[SP_GET_EMPLEADO_DETALLE]
    @EMPLEADO_NO INT
AS
BEGIN
    SELECT * FROM PLANTILLA WHERE EMPLEADO_NO = @EMPLEADO_NO
END


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTORES_PLANTILLA]    Script Date: 05/02/2026 19:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SP_GET_DOCTORES_PLANTILLA]
(@nombreHospital NVARCHAR(50), @suma int OUT, @media int OUT, @personas int OUT)
as

	SELECT @suma = isnull(SUM(SALARIO), 0), @media = isnull(AVG(SALARIO), 0), @personas = isnull(COUNT(APELLIDO), 0)
	FROM (SELECT APELLIDO, ESPECIALIDAD, SALARIO
	FROM DOCTOR WHERE HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE = @nombreHospital)
	union SELECT APELLIDO, FUNCION, SALARIO
	FROM PLANTILLA WHERE HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE = @nombreHospital))
	AS DatosEmpleados

	SELECT APELLIDO, ESPECIALIDAD, SALARIO
	FROM DOCTOR WHERE HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE = @nombreHospital)
	union
	SELECT APELLIDO, FUNCION, SALARIO
	FROM PLANTILLA WHERE HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE = @nombreHospital)


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_EMPLEADOS_DEPARTAMENTOS_OUT]    Script Date: 05/02/2026 19:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SP_EMPLEADOS_DEPARTAMENTOS_OUT]
(@nombre NVARCHAR(50), @suma int OUT, @media int OUT, @personas int OUT)
as
	declare @iddept int
	select @iddept = DEPT_NO from DEPT where DNOMBRE=@nombre

	select * from EMP where DEPT_NO=@iddept

	select @suma = isnull(SUM(SALARIO), 0), @media = isnull(AVG(SALARIO), 0), @personas = COUNT(EMP_NO)
	FROM EMP WHERE DEPT_NO = @iddept


USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_PLANTILLA]    Script Date: 05/02/2026 19:27:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 2. SP DELETE (El que te fallaba)
ALTER   PROCEDURE [dbo].[SP_DELETE_PLANTILLA]
    @EMPLEADO_NO INT
AS
BEGIN
    -- Intentamos borrar. Si falla, es probable que haya una restricción FK.
    DELETE FROM PLANTILLA WHERE EMPLEADO_NO = @EMPLEADO_NO
END



USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_ALL_EMPLEADOS_OUT]    Script Date: 05/02/2026 19:27:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SP_ALL_EMPLEADOS_OUT]
 ( @nombre nvarchar(50),
               @suma int OUT,
               @media int OUT,
               @personas int OUT)
               as
                  declare @iddept int
                  select @iddept=DEPT_NO from DEPT where DNOMBRE=@nombre
                  -- La consulta del procedimiento
                  select* from EMP where DEPT_NO=@iddept
                  --RELLENAMOS LAS VARIABLES DE SALIDA
                  select @suma= SUM( SALARIO), @media= AVG( SALARIO),
                  @personas=COUNT( EMP_NO) from EMP where DEPT_NO = @iddept



USE [HOSPITAL]
GO
/****** Object:  StoredProcedure [dbo].[SP_ALL_DEPARTAMENTOS]    Script Date: 05/02/2026 19:27:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[SP_ALL_DEPARTAMENTOS]
as
	SELECT * FROM DEPT
