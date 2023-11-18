-- Creación de la base de datos:
CREATE DATABASE LabComputadoras2


-- Cración del Login y contraseña para el usuario de la base de datos:
CREATE LOGIN usrComputadoras2 WITH PASSWORD='C0MPUMUND0',
  DEFAULT_DATABASE = LabComputadoras2,
  CHECK_EXPIRATION = OFF,
  CHECK_POLICY = ON
GO

USE LabComputadoras2
CREATE USER usrComputadoras2 FOR LOGIN usrComputadoras2
GO
ALTER ROLE db_owner ADD MEMBER usrComputadoras2
GO

-- Creación de las tablas:
CREATE TABLE Producto (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  codigo VARCHAR(10) NOT NULL,
  descripcion VARCHAR(120) NOT NULL,
  marca VARCHAR(30) NOT NULL,
  categoria VARCHAR(40) NOT NULL,
  precioVenta DECIMAL NOT NULL CHECK(precioVenta > 0),
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
);
CREATE TABLE Cliente (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  cedulaIdentidad VARCHAR(15) NOT NULL,
  nombres VARCHAR(40) NOT NULL,
  apellidos VARCHAR(40) NOT NULL,
  telefono VARCHAR(20) NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
CREATE TABLE Empleado (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  cedulaIdentidad VARCHAR(15) NOT NULL,
  nombres VARCHAR(50) NOT NULL,
  apellidos VARCHAR(50) NOT NULL,
  direccion VARCHAR(200) NOT NULL,
  celular INT NOT NULL,
  cargo VARCHAR(30) NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1
);
CREATE TABLE Usuario (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idEmpleado INT NOT NULL,
  nombreUsuario VARCHAR(15) NOT NULL,
  clave VARCHAR(30) NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Usuario_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)
);
CREATE TABLE Venta (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idUsuario INT NOT NULL,
  idCliente INT NOT NULL,
  fecha DATE NOT NULL DEFAULT GETDATE(),
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
  CONSTRAINT fk_Venta_CLiente FOREIGN KEY(idCliente) REFERENCES Cliente(id)
);
CREATE TABLE VentaDetalle (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idProducto INT NOT NULL,
  idVenta INT NOT NULL,
  precioUnitario DECIMAL NOT NULL,
  cantidad INT NOT NULL CHECK(cantidad > 0),
  total DECIMAL NOT NULL,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  estado SMALLINT NOT NULL DEFAULT 1,
  CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id),
  CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id)
);

-- Procedimientos almacenados creados

CREATE PROC paProductoListar @parametro VARCHAR(50)
AS
  SELECT id,codigo,descripcion,marca,categoria,precioVenta,usuarioRegistro,fechaRegistro,estado
  FROM Producto
  WHERE estado<>-1 AND descripcion LIKE '%'+REPLACE(@parametro,' ','%')+'%';
CREATE PROC paClienteListar @parametro VARCHAR(50)
AS
  SELECT id,cedulaIdentidad,nombres,apellidos,telefono,usuarioRegistro,fechaRegistro,estado
  FROM Cliente
  WHERE estado<>-1 AND nombres LIKE '%'+REPLACE(@parametro,' ','%')+'%';
CREATE PROC paEmpleadoListar @parametro VARCHAR(50)
AS
  SELECT id,cedulaIdentidad,nombres,apellidos,direccion,celular,cargo,usuarioRegistro,fechaRegistro,estado
  FROM Empleado
  WHERE estado<>-1 AND nombres LIKE '%'+REPLACE(@parametro,' ','%')+'%';
CREATE PROC paUsuarioListar @parametro VARCHAR(50)
AS
  SELECT us.id,idEmpleado,emp.cedulaIdentidad,us.nombreUsuario,us.clave,
  us.usuarioRegistro,us.fechaRegistro,us.estado

  FROM Usuario us INNER JOIN Empleado emp ON us.idEmpleado = emp.id
  WHERE us.estado<>-1 AND emp.cedulaIdentidad LIKE '%'+REPLACE(@parametro,' ','%')+'%';