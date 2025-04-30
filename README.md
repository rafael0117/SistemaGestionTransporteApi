# Sistema de Gestion y Transporte Api

<img width="30%" align="right" alt="Github" src="https://user-images.githubusercontent.com/48678280/88862734-4903af80-d201-11ea-968b-9c939d88a37c.gif" />

- ### Lenguaje y Tool
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" alt="C#" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/visualstudio/visualstudio-original.svg" alt="C#" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/git/git-original.svg" alt="C#" width="40" height="40"/>
  
- ### Database **SQL**
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-original.svg" alt="C#" width="40" height="40"/>

------------------------------------------------------------------------------------
- ### LOS USP :
  use bdtransporte
  go
------------------------------------------------------------------------------------
  CREATE PROCEDURE usp_insertar_venta_pasaje
      @fecha_venta DATETIME2,
      @total FLOAT,
      @id_usuario BIGINT,
      @numero VARCHAR(255),
      @estado VARCHAR(50) = 'pendiente'
  AS
  BEGIN
      INSERT INTO venta_pasaje (fecha_venta, total, id_usuario, numero, estado)
      VALUES (@fecha_venta, @total, @id_usuario, @numero, @estado);
  END
  GO
------------------------------------------------------------------------------------
  CREATE PROCEDURE usp_insertar_detalle_venta_pasaje
      @cantidad INT,
      @precio FLOAT,
      @total FLOAT,
      @id_venta INT,
      @id_viaje INT
  AS
  BEGIN
      INSERT INTO detalle_venta_pasaje (cantidad, precio, total, id_venta, id_viaje)
      VALUES (@cantidad, @precio, @total, @id_venta, @id_viaje);
  END
  GO
------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
  CREATE PROCEDURE usp_insertar_bus
      @modelo VARCHAR(50),
      @marca VARCHAR(50),
      @anio INT,
      @capacidad INT,
      @placa VARCHAR(20)
  AS
  BEGIN
      INSERT INTO bus (modelo, marca, anio, capacidad, placa)
      VALUES (@modelo, @marca, @anio, @capacidad, @placa);
  END
  GO
----------------------------------------------
  CREATE PROCEDURE usp_actualizar_bus
      @id_bus INT,
      @modelo VARCHAR(50),
      @marca VARCHAR(50),
      @anio INT,
      @capacidad INT,
      @placa VARCHAR(20)
  AS
  BEGIN
      UPDATE bus
      SET modelo = @modelo,
          marca = @marca,
          anio = @anio,
          capacidad = @capacidad,
          placa = @placa
      WHERE id_bus = @id_bus;
  END
  GO
----------------------------------------------

  CREATE PROCEDURE usp_eliminar_bus
      @id_bus INT
  AS
  BEGIN
      DELETE FROM bus WHERE id_bus = @id_bus;
  END
  GO

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
  CREATE PROCEDURE usp_insertar_destino
      @nombre_des VARCHAR(100),
      @imagen VARCHAR(255) NULL
  AS
  BEGIN
      INSERT INTO destino (nombre_des, imagen)
      VALUES (@nombre_des, @imagen);
  END
  GO
----------------------------------------------
  CREATE PROCEDURE usp_actualizar_destino
      @id_destino INT,
      @nombre_des VARCHAR(100),
      @imagen VARCHAR(255) NULL
  AS
  BEGIN
      UPDATE destino
      SET nombre_des = @nombre_des,
          imagen = @imagen
      WHERE id_destino = @id_destino;
  END
  GO
----------------------------------------------
  CREATE PROCEDURE usp_eliminar_destino
      @id_destino INT
  AS
  BEGIN
      DELETE FROM destino WHERE id_destino = @id_destino;
  END
  GO

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
  CREATE PROCEDURE usp_insertar_viaje
      @id_bus INT,
      @id_destino INT,
      @fech_sal DATE,
      @fech_lle DATE,
      @incidencias VARCHAR(40) NULL,
      @precio FLOAT
  AS
  BEGIN
      INSERT INTO viaje (id_bus, id_destino, fech_sal, fech_lle, incidencias, precio)
      VALUES (@id_bus, @id_destino, @fech_sal, @fech_lle, @incidencias, @precio);
  END
  GO
----------------------------------------------
  CREATE PROCEDURE usp_actualizar_viaje
      @id_viaje INT,
      @id_bus INT,
      @id_destino INT,
      @fech_sal DATE,
      @fech_lle DATE,
      @incidencias VARCHAR(40) NULL,
      @precio FLOAT
  AS
  BEGIN
      UPDATE viaje
      SET id_bus = @id_bus,
          id_destino = @id_destino,
          fech_sal = @fech_sal,
          fech_lle = @fech_lle,
          incidencias = @incidencias,
          precio = @precio
      WHERE id_viaje = @id_viaje;
  END
  GO
----------------------------------------------
  CREATE PROCEDURE usp_eliminar_viaje
      @id_viaje INT
  AS
  BEGIN
      DELETE FROM viaje WHERE id_viaje = @id_viaje;
  END
  GO
----------------------------------------------

  /*RESET VALOR DE TABLA*/
  DBCC CHECKIDENT ('bus', RESEED, #);

------------------------------------------------------------------------------------
