-- Script para poblar la tabla Cliente
-- Insertar datos de clientes
INSERT INTO Cliente (nombre, apellidos, fecha_nacimiento, cedula, telefono, email)
VALUES
  ('Juan', 'Pérez', '2000-05-15', '1234567890', '555-1234', 'juan.perez@email.com'),
  ('Ana', 'Gómez', '1995-09-22', '9876543210', '555-5678', 'ana.gomez@email.com'),
  ('Carlos', 'Martínez', '1988-07-10', '1357924680', '555-9876', 'carlos.martinez@email.com'),
  ('Luisa', 'Hernández', '1985-03-30', '2468013579', '555-5432', 'luisa.hernandez@email.com'),
  ('Sofía', 'Ramírez', '1977-11-18', '9876543211', '555-8765', 'sofia.ramirez@email.com'),
  ('Pedro', 'Díaz', '1972-06-05', '1234567891', '555-2345', 'pedro.diaz@email.com'),
  ('Laura', 'Gutiérrez', '1965-12-12', '1357924681', '555-6789', 'laura.gutierrez@email.com'),
  ('Manuel', 'López', '1960-08-08', '2468013580', '555-4321', 'manuel.lopez@email.com');

-- Script para poblar la tabla Videojuegos
-- Insertar datos de videojuegos
INSERT INTO Videojuego(nombre, año, protagonistas, director, productor, plataforma)
VALUES
  ('Batman: Arkham Knight', 2015, 'Batman', 'Sefton Hill', 'Warner Bros', 2),
  ('The Legend of Zelda: Tears of the Kingdom', 2023, 'Link', 'Hidemaro Fujibayashi', 'Eiji Aonuma', 3),
  ('Halo Infinite', 2021, 'Jefe Maestro', 'Joseph Staten', '343 Industries', 1),
  ('Mortal Kombat 1', 2023, 'Liu Kang', 'Ed Boon', 'Warner Bros', 4),
  ('Assassin''s Creed: Valhalla', 2020, 'Eivor', 'Darby McDevitt', 'Ubisoft', 1),
  ('Cyberpunk 2077', 2020, 'V', 'Adam Badowski', 'CD Projekt', 4),
  ('Super Mario Odyssey', 2017, 'Mario', 'Kenta Motokura', 'Nintendo', 3),
  ('Fortnite', 2017, 'Varios', 'Epic Games', 'Epic Games', 1),
  ('Overwatch', 2016, 'Varios', 'Jeff Kaplan', 'Blizzard Entertainment', 4),
  ('Red Dead Redemption 2', 2018, 'Arthur Morgan', 'Dan Houser', 'Rockstar Games', 2),
  ('Far Cry 6', 2021, 'Dani Rojas', 'Navid Khavari', 'Ubisoft', 2);

-- Script para poblar la tabla Precio_videojuego con valores fijos
-- Insertar datos de precios de videojuegos
INSERT INTO Precio_videojuego (idJuego, precio_dia)
VALUES
  (1, 75000.00), -- Batman: Arkham Knight
  (2, 85000.00), -- The Legend of Zelda: Tears of the Kingdom
  (3, 70000.00), -- Halo Infinite
  (4, 60000.00), -- Mortal Kombat 1
  (5, 90000.00), -- Assassin's Creed: Valhalla
  (6, 95000.00), -- Cyberpunk 2077
  (7, 80000.00), -- Super Mario Odyssey
  (8, 70000.00), -- Fortnite
  (9, 75000.00), -- Overwatch
  (10, 85000.00), -- Red Dead Redemption 2
  (11, 90000.00); -- Far Cry 6

-- Script para poblar la tabla Alquiler
-- Insertar datos de alquileres
INSERT INTO Alquiler (idCliente, idJuego, fecha_ini, fecha_dev, precio_juego, precio_total, estado)
VALUES
  -- 5 registros con fecha_dev posterior al día de hoy y estado en 0
  (1, 1, GETDATE(), DATEADD(day, 5, GETDATE()), 75000.00, 5 * 75000.00, 0),
  (2, 2, GETDATE(), DATEADD(day, 5, GETDATE()), 85000.00, 5 * 85000.00, 0),
  (3, 3, GETDATE(), DATEADD(day, 4, GETDATE()), 70000.00, 4 * 70000.00, 0),
  (4, 4, GETDATE(), DATEADD(day, 4, GETDATE()), 60000.00, 4 * 60000.00, 0),
  (5, 5, GETDATE(), DATEADD(day, 5, GETDATE()), 90000.00, 5 * 90000.00, 0),

  -- 10 registros con fecha_dev anterior al día de hoy y estado en 1
  (6, 6, '2022-01-10', '2022-01-13', 95000.00, 3 * 95000.00, 1),
  (7, 7, '2022-02-05', '2022-02-07', 80000.00, 2 * 80000.00, 1),
  (8, 8, '2022-03-15', '2022-03-16', 70000.00, 1 * 70000.00, 1),
  (8, 9, '2022-04-20', '2022-04-22', 75000.00, 2 * 75000.00, 1),
  (1, 10, '2022-05-25', '2022-05-28', 85000.00, 3 * 85000.00, 1),
  (3, 11, '2022-06-30', '2022-07-02', 90000.00, 2 * 90000.00, 1),
  (1, 3, '2022-08-05', '2022-08-07', 75000.00, 2 * 75000.00, 1),
  (1, 2, '2022-09-10', '2022-09-11', 85000.00, 1 * 85000.00, 1),
  (3, 2, '2022-10-15', '2022-10-17', 70000.00, 2 * 70000.00, 1),
  (4, 2, '2022-11-20', '2022-11-24', 60000.00, 4 * 60000.00, 1);
